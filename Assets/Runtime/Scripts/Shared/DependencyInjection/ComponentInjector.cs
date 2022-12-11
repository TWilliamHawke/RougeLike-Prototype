using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;

public sealed class ComponentInjector : MonoBehaviour, IInjectionTarget
{
    [SerializeField] MonoBehaviour _injectionTarget;
    [SerializeField] bool _waitForAllDependencies;
    [SerializeField] Injector[] _injectors;

    [SerializeField] UnityEvent _finalizeInjectionHandler;

    public bool waitForAllDependencies => _waitForAllDependencies;

    bool _targetAdded = false;

    private void Awake()
    {
        TryAddTarget();
    }

    private void OnEnable()
    {
        TryAddTarget();
    }

    private void TryAddTarget()
    {
        if (_targetAdded) return;
        _targetAdded = true;

        foreach (var injector in _injectors)
        {
            injector.AddInjectionTarget(this);
        }
    }

    void IInjectionTarget.FinalizeInjection()
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


