using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;

[CreateAssetMenu(fileName = "Injector", menuName = "Musc/Injector")]
public class Injector : ScriptableObject
{
    [SerializeField] bool _destroyOnLoad = false;
    IDependency _dependencyWithState;
    System.Object _dependency;

    bool _dependencyIsReady => _dependencyWithState?.isReadyForUse ?? true;

    List<IInjectionTarget> _targetsWaitingForInjection = new List<IInjectionTarget>();
    List<IInjectionTarget> _targetsWaitingForFinalization = new List<IInjectionTarget>();

    public void TryDestroyDependency()
    {
        if (_destroyOnLoad)
        {
            _dependencyWithState = null;
        }
    }

    public void AddDependency(System.Object dependency)
    {
        if (_dependency != null) return;
        _dependency = dependency;
        InjectForAll();
    }

    public void AddDependencyWithState(IDependency dependency)
    {
        //SingleTone like
        if (_dependency != null) return;
        _dependency = _dependencyWithState = dependency;
        InjectForAll();

        if (!dependency.isReadyForUse)
        {
            dependency.OnReadyForUse += FinalizeAllTargets;
        }
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

    private void InjectForAll()
    {
        foreach (var target in _targetsWaitingForInjection)
        {
            Inject(target);
        }
        _targetsWaitingForInjection.Clear();
    }


    void Inject(IInjectionTarget injectionTarget)
    {
        var fields = FindAllFieldsWithAttribute(injectionTarget);

        foreach(var field in fields)
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

    void FinalizeAllTargets()
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
    void Finalize(IInjectionTarget injectionTarget)
    {
        if(!injectionTarget.waitForAllDependencies)
        {
            injectionTarget.FinalizeInjection();
            return;
        }
        
        var fields = FindAllFieldsWithAttribute(injectionTarget);

        foreach(var field in fields)
        {
            if (field.GetValue(injectionTarget) is null) return;
        }

        injectionTarget.FinalizeInjection();
    }

    List<FieldInfo> FindAllFieldsWithAttribute(IInjectionTarget injectionTarget)
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

