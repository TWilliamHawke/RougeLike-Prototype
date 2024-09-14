using UnityEngine;
using UnityEngine.Events;
using System.Reflection;
using Map.Actions;

public sealed class ComponentInjector : MonoBehaviour, IInjectionTarget, IManualInjector
{
    [SerializeField] MonoBehaviour _injectionTarget;
    [SerializeField] bool _waitForAllDependencies;
    [SerializeField] bool _addTargetManualy;
    [SerializeField] Injector[] _injectors;

    [SerializeField] UnityEvent _finalizeInjectionHandler;

    bool IInjectionTarget.waitForAllDependencies => false;
    MonoBehaviour IManualInjector.target => _injectionTarget;
    int _remainedDependencies = 0;

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
        _remainedDependencies = _injectors.Length;

        foreach (var injector in _injectors)
        {
            injector.AddInjectionTarget(this);
        }
    }

    public object GetRealTarget()
    {
        return _injectionTarget;
    }

    private void TryAddTarget()
    {
        if (_targetAdded || _addTargetManualy) return;
        AddTarget();
    }

    void IInjectionTarget.FinalizeInjection()
    {
        _remainedDependencies--;
        if (_remainedDependencies > 0) return;
        _finalizeInjectionHandler?.Invoke();
    }

    System.Type IInjectionTarget.GetType()
    {
        return _injectionTarget.GetType();
    }

    void IInjectionTarget.SetFieldValue(FieldInfo field, object dependency)
    {
        field.SetValue(_injectionTarget, dependency);
    }

    bool IInjectionTarget.FieldIsNull(FieldInfo field)
    {
        return field.GetValue(_injectionTarget) is null;
    }
}


