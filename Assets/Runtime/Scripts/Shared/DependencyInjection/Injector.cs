using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Injector", menuName = "Musc/Injector")]
public class Injector : ScriptableObject
{
    static event UnityAction<object> OnInjectionFinalize;
    static event UnityAction<object> OnInjectionSuccess;
    object _dependency;

    bool _dependencyIsReady = true;
    DependencyReadyTrigger _readyTrigger;
    int _emptyInjectFields = 0;

    HashSet<IInjectionTarget> _targetsWaitingForInjection = new HashSet<IInjectionTarget>();
    HashSet<IInjectionTarget> _targetsWaitingForFinalization = new HashSet<IInjectionTarget>();
    List<FieldInfo> _injectFieldCache = new(5);

    public void ClearDependency()
    {
        _targetsWaitingForInjection.Clear();
        _targetsWaitingForFinalization.Clear();
        OnInjectionFinalize -= FinalizeAfterFinalization;

        if (_dependency is IPermanentDependency)
        {
            (_dependency as IPermanentDependency).ClearState();
        }
        else
        {
            _dependency = null;
        }
    }

    public void SetDependency<T>(ref T dependency) where T : class, IPermanentDependency
    {
        if (_dependency is null)
        {
            SetDependency(dependency);
        }
        dependency = _dependency as T ?? dependency;
    }

    public void SetDependency(object dependency,
        DependencyReadyTrigger trigger = DependencyReadyTrigger.allInjectFieldsIsFull)
    {
        if (_dependency is not null) return;
        _dependency = dependency;
        _readyTrigger = trigger;
        _emptyInjectFields = GetInjectFieldsCount(dependency);

        if (_emptyInjectFields > 0)
        {
            _dependencyIsReady = false;
            OnInjectionSuccess += FinalizeAfterInjection;
            OnInjectionFinalize += FinalizeAfterFinalization;
        }

        InjectForWaitingTargets();
    }

    //add object that required this dependency
    public void AddInjectionTarget(IInjectionTarget injectionTarget)
    {
        var fields = FindAllInjectFields(injectionTarget.GetType());

        if (fields.Count <= 0)
        {
            Debug.LogError($"Wrong target for injection: {injectionTarget.GetType().Name}");
            return;
        }

        //if SetDependency already called - inject immediately
        //else - add object to queue
        if (_dependency is null)
        {
            _targetsWaitingForInjection.Add(injectionTarget);
        }
        else
        {
            Inject(injectionTarget, fields);
        }
    }

    private void FinalizeAfterInjection(object injectionTarget)
    {
        if (injectionTarget != _dependency) return;
        _emptyInjectFields--;

        if (_emptyInjectFields > 0) return;
        FinalizeTargets(DependencyReadyTrigger.allInjectFieldsIsFull);
        OnInjectionSuccess -= FinalizeAfterInjection;
    }

    //calls if dependency in this injector becoming ready
    private void FinalizeAfterFinalization(object possibleDependency)
    {
        if (possibleDependency != _dependency) return;
        if (_emptyInjectFields > 0) return;

        FinalizeTargets(DependencyReadyTrigger.finalization);
        OnInjectionFinalize -= FinalizeAfterFinalization;
    }

    private void FinalizeTargets(DependencyReadyTrigger condition)
    {
        if (_readyTrigger != condition) return;
        _dependencyIsReady = true;

        foreach (var target in _targetsWaitingForFinalization)
        {
            Finalize(target);
        }

        _targetsWaitingForFinalization.Clear();
    }

    //calls then dependency added to injector
    private void InjectForWaitingTargets()
    {
        foreach (var target in _targetsWaitingForInjection)
        {
            Inject(target);
        }
        _targetsWaitingForInjection.Clear();
    }

    private void Inject(IInjectionTarget injectionTarget)
    {
        var fields = FindAllInjectFields(injectionTarget.GetType());
        Inject(injectionTarget, fields);
    }

    private void Inject(IInjectionTarget injectionTarget, IEnumerable<FieldInfo> fields)
    {
        foreach (var field in fields)
        {
            if (!field.FieldType.IsAssignableFrom(_dependency.GetType())) continue;
            injectionTarget.SetFieldValue(field, _dependency);
            OnInjectionSuccess?.Invoke(injectionTarget.GetRealTarget());
        }

        if (_dependencyIsReady)
        {
            Finalize(injectionTarget);
        }
        else
        {
            _targetsWaitingForFinalization.Add(injectionTarget);
        }
    }

    //FinalizeInjection will call only if all injections is done
    //(fields with InjectFieldAttribute not null)
    //and calls immediately after last injection
    private void Finalize(IInjectionTarget injectionTarget)
    {
        if (injectionTarget.waitForAllDependencies
            && AnyInjectFieldIsNull(injectionTarget)) return;

        injectionTarget.FinalizeInjection();

        //key part. starts the chain of FinalizeInjection call
        OnInjectionFinalize?.Invoke(injectionTarget.GetRealTarget());
    }

    private int GetInjectFieldsCount(object obj)
    {
        if (_dependency is not IInjectionTarget && _readyTrigger == DependencyReadyTrigger.adding) return 0;

        if (obj is IInjectionTarget injectionTarget)
        {
            var fields = FindAllInjectFields(injectionTarget.GetType());
            return fields.Count(field => injectionTarget.FieldIsNull(field));
        }
        else
        {
            var fields = FindAllInjectFields(obj.GetType());
            return fields.Count(field => field.GetValue(obj) == null);
        }

    }

    private bool AnyInjectFieldIsNull(IInjectionTarget injectionTarget)
    {
        var fields = FindAllInjectFields(injectionTarget.GetType());
        return fields.Any(field => injectionTarget.FieldIsNull(field));
    }

    private List<FieldInfo> FindAllInjectFields(Type type)
    {
        _injectFieldCache.Clear();

        while (type != null)
        {
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.DeclaredOnly | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (field.GetCustomAttribute<InjectFieldAttribute>(false) is null) continue;
                _injectFieldCache.Add(field);
            }
            type = type.BaseType;
        }

        return _injectFieldCache;
    }

    private void LogType<T>(object obj, string message = "")
    {
        Type requiredType = typeof(T);
        Type objType = (obj as IInjectionTarget)?.GetType() ?? obj.GetType();
        if (objType == requiredType)
        {
            Debug.Log(message + " " + requiredType.Name);
        }
    }
}
