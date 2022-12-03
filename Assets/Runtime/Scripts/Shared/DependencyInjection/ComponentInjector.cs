using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;

public sealed class ComponentInjector : MonoBehaviour, IInjectionTarget
{
    [SerializeField] MonoBehaviour _injectionTarget;
    [SerializeField] Injector[] _injectors;
    [SerializeField] bool _waitForAllDependencies;

    [SerializeField] UnityEvent _finalizeInjectionHandler;

    public bool waitForAllDependencies => _waitForAllDependencies;

    private void Awake()
    {
        foreach (var injector in _injectors)
        {
            injector.AddInjectionTarget(this);
        }
    }

    public void FinalizeInjection()
    {
        _finalizeInjectionHandler?.Invoke();
    }

    System.Type IInjectionTarget.GetType()
    {
        return _injectionTarget.GetType();
    }

    void IInjectionTarget.SetValue(FieldInfo field, object dependency)
    {
        field.SetValue(_injectionTarget, dependency);
    }

    bool IInjectionTarget.FieldIsNull(FieldInfo field)
    {
        return field.GetValue(_injectionTarget) is null;
    }
}


