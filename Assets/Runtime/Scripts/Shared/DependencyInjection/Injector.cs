using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

[CreateAssetMenu(fileName = "Injector", menuName = "Musc/Injector")]
public class Injector : ScriptableObject
{
    [SerializeField] bool _clearOnLoad = true;
    IDependency _dependencyWithState;
    System.Object _dependency;

    bool _dependencyIsReady => _dependencyWithState?.isReadyForUse ?? true;

    HashSet<IInjectionTarget> _targetsWaitingForInjection = new HashSet<IInjectionTarget>();
    HashSet<IInjectionTarget> _targetsWaitingForFinalization = new HashSet<IInjectionTarget>();

    public void TryDestroyDependency()
    {
        if (_clearOnLoad)
        {
            _dependencyWithState = null;
        }
    }

    public void AddDependency(System.Object dependency)
    {
        if (_dependency != null) return;
        _dependency = dependency;
        if (dependency is IDependency)
        {
            AddDependencyWithState(dependency as IDependency);
            return;
        }
        InjectForAll();
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

    private void AddDependencyWithState(IDependency dependency)
    {
        _dependencyWithState = dependency;
        //should set _dependencyWithState before inject
        InjectForAll();

        if (!dependency.isReadyForUse)
        {
            dependency.OnReadyForUse += FinalizeAllTargets;
        }
    }

    private void InjectForAll()
    {
        foreach (var target in _targetsWaitingForInjection)
        {
            Inject(target);
        }
        _targetsWaitingForInjection.Clear();
    }


    private void Inject(IInjectionTarget injectionTarget)
    {
        var fields = FindAllFieldsWithAttribute(injectionTarget);

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

    private void FinalizeAllTargets()
    {
        if (!_dependencyIsReady)
        {
            Debug.LogError("OnReadyForUse called before object is ready!");
        }

        foreach (var target in _targetsWaitingForFinalization)
        {
            Finalize(target);
        }
        _targetsWaitingForFinalization.Clear();
        _dependencyWithState.OnReadyForUse -= FinalizeAllTargets;
    }

    //FinalizeInjection will call only if all injections is done
    //(fields with InjectFieldAttribute not null)
    private void Finalize(IInjectionTarget injectionTarget)
    {
        if (!injectionTarget.waitForAllDependencies)
        {
            injectionTarget.FinalizeInjection();
            return;
        }

        var fields = FindAllFieldsWithAttribute(injectionTarget);

        foreach (var field in fields)
        {
            if (field.GetValue(injectionTarget) is null) return;
        }

        injectionTarget.FinalizeInjection();
    }

    private List<FieldInfo> FindAllFieldsWithAttribute(IInjectionTarget injectionTarget)
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

