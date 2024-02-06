using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using ReduceCallback = System.Func<float, float, float>;

public class ValueStorage : IValueStorage
{
    public int currentValue => _currentValue;
    public int maxValue => _maxValue;
    public int minValue => _minValue;

    Dictionary<IBonusValueSource, BonusValueData> _bonusValues = new();

    int _maxValue;
    int _currentValue;
    int _minValue;
    float _pctOfMax = 0f;
    bool _flatFirst = true;

    public event UnityAction<int> OnValueChange;
    public event UnityAction OnReachMax;
    public event UnityAction OnReachMin;

    public ValueStorage(int minValue, int maxValue, int startValue, bool flatFirst = true)
    {
        _maxValue = maxValue;
        _minValue = minValue;
        SetNewValue(startValue);
        _flatFirst = flatFirst;
    }

    public ValueStorage() : this(0, int.MaxValue, 0)
    {
    }

    public void SetNewValue(float newValue)
    {
        if (_currentValue == newValue) return;

        var oldPctOfMax = _pctOfMax;
        _currentValue = NormalizeValue(newValue);
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

    public void AddBonusValue(IBonusValueSource valueSource, BonusValueType bonusType, float bonus)
    {
        RemoveBonusValue(valueSource);
        _bonusValues.Add(valueSource, new BonusValueData(bonus, bonusType));
    }

    public void RemoveBonusValue(IBonusValueSource valueSource)
    {
        _bonusValues.Remove(valueSource);
    }

    public void IncreaseValue(int change)
    {
        SetNewValue(_currentValue + change);
    }

    public void ReduceValue(int change)
    {
        SetNewValue(_currentValue - change);
    }

    public bool TryReduceValue(int value)
    {
        int newValue = _currentValue - value;
        if (newValue < minValue) return false;

        SetNewValue(newValue);
        return true;
    }

    public int GetFinalValue()
    {
        float flatBonus = GetBonusValue(BonusValueType.flat, 0, (acc, val) => acc + val);
        float pctBonus = GetBonusValue(BonusValueType.percentage, 1, (acc, val) => acc + val / 100);
        float multBonus = GetBonusValue(BonusValueType.mult, 1, (acc, val) => acc * val);
        float finalValue = _flatFirst ? (_currentValue + flatBonus) * pctBonus : _currentValue * pctBonus + flatBonus;

        return NormalizeValue(finalValue * multBonus);
    }

    public ValueState GetState()
    {
        int numericState = (int)Mathf.Sign(GetFinalValue() - _currentValue);
        return (ValueState)numericState;
    }

    private float GetBonusValue(BonusValueType valueType, float startValue, ReduceCallback callback)
    {
        foreach (var pair in _bonusValues)
        {
            if (pair.Value.valueBonusType != valueType) continue;
            float bonus = pair.Value.value;
            startValue = callback(startValue, bonus);
        }

        return startValue;
    }

    private int NormalizeValue(float value)
    {
        value = Mathf.Clamp(value, _minValue, _maxValue);
        return Mathf.RoundToInt(value);
    }
}
