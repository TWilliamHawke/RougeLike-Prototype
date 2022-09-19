using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

[CreateAssetMenu(fileName = "Injector", menuName = "Musc/Injector")]
public class Injector : ScriptableObject
{
    IDependency _dependency;

    List<IInjectionTarget> _targetsWaitingForInjection = new List<IInjectionTarget>();
    List<IInjectionTarget> _targetsWaitingForFinalization = new List<IInjectionTarget>();

    public void AddDependency(IDependency dependency)
    {
        //SingleTone like
        if (_dependency != null) return;
        _dependency = dependency;

        if (!dependency.isReadyForUse)
        {
            _dependency.OnReadyForUse += FinalizeTargets;
        }

        foreach (var target in _targetsWaitingForInjection)
        {
            Inject(target);
        }
        _targetsWaitingForInjection.Clear();
    }

    public void AddInjectionTarget(IInjectionTarget injectionTarget)
    {
        if (_dependency is null)
        {
            _targetsWaitingForInjection.Add(injectionTarget);
        }
        else
        {
            Inject(injectionTarget);
        }
    }


    void Inject(IInjectionTarget injectionTarget)
    {
        Type type = injectionTarget.GetType();
        while (type != null)
        {
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic
                | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            foreach (var field in fields)
            {
                if (field.GetCustomAttribute<InjectFieldAttribute>(false) is null
                    || !field.FieldType.IsAssignableFrom(_dependency.GetType())) continue;

                field.SetValue(injectionTarget, _dependency);
            }
            type = type.BaseType;
        }

        if (_dependency.isReadyForUse)
        {
            injectionTarget.FinalizeInjection();
        }
        else
        {
            _targetsWaitingForFinalization.Add(injectionTarget);
        }
    }

    void FinalizeTargets()
    {
        if (!_dependency.isReadyForUse)
        {
            Debug.LogError("OnReadyForUse called before object is ready!");
        }

        foreach (var target in _targetsWaitingForFinalization)
        {
            target.FinalizeInjection();
        }
        _targetsWaitingForFinalization.Clear();
        _dependency.OnReadyForUse -= FinalizeTargets;
    }
}

