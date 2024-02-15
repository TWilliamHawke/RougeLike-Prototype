using UnityEngine;
using UnityEngine.Events;
using System;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Injector", menuName = "Musc/Injector")]
public class Injector : ScriptableObject
{
    static event UnityAction<IInjectionTarget> OnInjectionFinalize;
    System.Object _dependency;

    bool _dependencyIsReady = true;

    HashSet<IInjectionTarget> _targetsWaitingForInjection = new HashSet<IInjectionTarget>();
    HashSet<IInjectionTarget> _targetsWaitingForFinalization = new HashSet<IInjectionTarget>();

    public void ClearDependency()
    {
        _targetsWaitingForInjection.Clear();
        _targetsWaitingForFinalization.Clear();
        OnInjectionFinalize -= FinalizeTargetsIfDependencyIsReady;

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

    public void SetDependency(object dependency)
    {
        if (_dependency is not null) return;
        _dependency = dependency;

        if (dependency is IInjectionTarget && AnyInjectFieldIsNull(dependency))
        {
            _dependencyIsReady = false;
            OnInjectionFinalize += FinalizeTargetsIfDependencyIsReady;
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

    //calls if dependency in this injector becoming ready
    private void FinalizeTargetsIfDependencyIsReady(IInjectionTarget possibleDependency)
    {
        if (possibleDependency != _dependency) return;
        if (AnyInjectFieldIsNull(_dependency)) return;
        _dependencyIsReady = true;

        foreach (var target in _targetsWaitingForFinalization)
        {
            Finalize(target);
        }

        _targetsWaitingForFinalization.Clear();
        OnInjectionFinalize -= FinalizeTargetsIfDependencyIsReady;
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
            injectionTarget.SetValue(field, _dependency);
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

        var realTarget = (injectionTarget as IDependencyProvider)?.realTarget ?? injectionTarget;

        //key part. starts the chain of FinalizeInjection call
        OnInjectionFinalize?.Invoke(realTarget);
    }

    private bool AnyInjectFieldIsNull(object dependency)
    {
        if (dependency is not IInjectionTarget) return false;
        return AnyInjectFieldIsNull(dependency as IInjectionTarget);
    }

    private bool AnyInjectFieldIsNull(IInjectionTarget injectionTarget)
    {
        var fields = FindAllInjectFields(injectionTarget.GetType());
        return fields.Any(field => injectionTarget.FieldIsNull(field));
    }

    private List<FieldInfo> FindAllInjectFields(Type type)
    {
        var fieldsWithAttribute = new List<FieldInfo>();

        while (type != null)
        {
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.DeclaredOnly | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (field.GetCustomAttribute<InjectFieldAttribute>(false) is null) continue;
                fieldsWithAttribute.Add(field);
            }
            type = type.BaseType;
        }

        return fieldsWithAttribute;
    }
}