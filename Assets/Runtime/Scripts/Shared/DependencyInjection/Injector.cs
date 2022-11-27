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

    public void SetDependency(System.Object dependency)
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

    public void AddInjectionTarget(IInjectionTarget injectionTarget)
    {
        var fields = FindAllInjectFields(injectionTarget);

        if (fields.Count <= 0)
        {
            Debug.LogError("Wrong target for injection");
            return;
        }

        if (_dependency is null)
        {
            _targetsWaitingForInjection.Add(injectionTarget);
        }
        else
        {
            Inject(injectionTarget, fields);
        }
    }

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
        var fields = FindAllInjectFields(injectionTarget);
        Inject(injectionTarget, fields);
    }

    private void Inject(IInjectionTarget injectionTarget, IEnumerable<FieldInfo> fields)
    {
        foreach (var field in fields)
        {
            if (!field.FieldType.IsAssignableFrom(_dependency.GetType())) continue;
            field.SetValue(injectionTarget, _dependency);
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
    private void Finalize(IInjectionTarget injectionTarget)
    {
        if (injectionTarget.waitForAllDependencies
            && AnyInjectFieldIsNull(injectionTarget)) return;

        injectionTarget.FinalizeInjection();
        OnInjectionFinalize?.Invoke(injectionTarget);
    }

    private bool AnyInjectFieldIsNull(System.Object injectionTarget)
    {
        var fields = FindAllInjectFields(injectionTarget);
        return fields.Any(field => field.GetValue(injectionTarget) is null);
    }

    private List<FieldInfo> FindAllInjectFields(System.Object injectionTarget)
    {
        var fieldsWithAttribute = new List<FieldInfo>();

        Type type = injectionTarget.GetType();
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