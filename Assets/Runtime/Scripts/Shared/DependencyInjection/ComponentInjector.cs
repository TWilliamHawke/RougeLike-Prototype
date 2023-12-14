using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Reflection;

public sealed class ComponentInjector : MonoBehaviour, IDependencyProvider, IManualInjector
{
    [SerializeField] MonoBehaviour _injectionTarget;
    [SerializeField] bool _waitForAllDependencies;
    [SerializeField] bool _addTargetManualy;
    [SerializeField] Injector[] _injectors;

    [SerializeField] UnityEvent _finalizeInjectionHandler;

    bool IInjectionTarget.waitForAllDependencies => _injectors.Length < 2 ? false : _waitForAllDependencies;
    IInjectionTarget IDependencyProvider.realTarget => _injectionTarget as IInjectionTarget;
    MonoBehaviour IManualInjector.target => _injectionTarget;

    bool _targetAdded = false;

    private void Start()
    {
        TryAddTarget();
    }

    private void OnEnable()
    {
        TryAddTarget();
    }

    public void AddTarget()
    {
        _targetAdded = true;

        foreach (var injector in _injectors)
        {
            injector.AddInjectionTarget(this);
        }
    }

    private void TryAddTarget()
    {
        if (_targetAdded || _addTargetManualy) return;
        AddTarget();
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


