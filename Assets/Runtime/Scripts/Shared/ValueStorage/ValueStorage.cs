using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ValueStorage : IValueStorage
{
    public int currentValue => _currentValue;
    public int maxValue => _maxValue;
    public int minValue => _minValue;

    int _maxValue;
    int _currentValue;
    int _minValue;
    float _pctOfMax = 0f;

    public event UnityAction<int> OnValueChange;
    public event UnityAction OnReachMax;
    public event UnityAction OnReachMin;

    public ValueStorage(int minValue, int maxValue, int currentValue)
    {
        _maxValue = maxValue;
        _minValue = minValue;
        SetNewValue(currentValue);
    }

    public void SetNewValue(int newValue)
    {
        if (_currentValue == newValue) return;

        var oldPctOfMax = _pctOfMax;
        _currentValue = Mathf.Clamp(newValue, _minValue, _maxValue);
        _pctOfMax = (float)_currentValue / (_maxValue - _minValue);
        OnValueChange?.Invoke(_currentValue);

        if (_currentValue == _minValue && oldPctOfMax > 0f)
        {
            OnReachMin?.Invoke();
        }

        if (_currentValue == _maxValue && oldPctOfMax < 1f)
        {
            OnReachMax?.Invoke();
        }
    }

    public void IncreaseValue(int change)
    {
        SetNewValue(_currentValue + change);
    }

    public void ReduceValue(int change)
    {
        SetNewValue(_currentValue - change);
    }

    public bool TryReduceStat(int value)
    {
        int newValue = _currentValue - value;
        if (newValue < minValue) return false;

        SetNewValue(newValue);
        return true;
    }



}

public interface IValueStorage
{
    int currentValue { get; }
    int maxValue { get; }
    event UnityAction<int> OnValueChange;
}
