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

    Dictionary<BonusValueType, IBonusValueLogic> _bonusValues = new()
    {
        { BonusValueType.flat, new FlatBonusValue() },
        { BonusValueType.percentage, new PercentageBonusValue() },
        { BonusValueType.mult, new MultBonusValue() }
    };

    int _maxValue;
    int _currentValue;
    int _minValue;
    bool _flatFirst = true;

    public event UnityAction<int> OnValueChange;
    public event UnityAction OnReachMax;
    public event UnityAction OnReachMin;

    public ValueStorage(int minValue, int maxValue, int startValue, bool flatFirst = true)
    {
        _maxValue = Mathf.Max(minValue, maxValue);
        _minValue = Mathf.Min(minValue, maxValue);
        SetNewValue(startValue);
        _flatFirst = flatFirst;
    }

    public ValueStorage() : this(0, int.MaxValue, 0) { }

    public void SetNewValue(float newValue)
    {
        if (_currentValue == newValue) return;

        int oldValue = _currentValue;
        _currentValue = NormalizeValue(newValue);
        OnValueChange?.Invoke(_currentValue);

        if (_currentValue == _minValue && oldValue > _minValue)
        {
            OnReachMin?.Invoke();
        }

        if (_currentValue == _maxValue && oldValue < _maxValue)
        {
            OnReachMax?.Invoke();
        }
    }

    public void AddBonusValue(BonusValueType bonusType, float bonus)
    {
        if (_bonusValues.TryGetValue(bonusType, out IBonusValueLogic bonusValue))
        {
            bonusValue.AddBonusValue(bonus);
        }
    }

    public void RemoveBonusValue(BonusValueType bonusType, float bonus)
    {
        if (_bonusValues.TryGetValue(bonusType, out IBonusValueLogic bonusValue))
        {
            bonusValue.RemoveBonusValue(bonus);
        }
    }

    public void ChangeValueBy(int change)
    {
        SetNewValue(GetSumSafe(_currentValue, change));
    }

    public bool TryReduceValue(int value)
    {
        int newValue = GetSumSafe(_currentValue, -value);
        if (newValue < minValue) return false;

        SetNewValue(newValue);
        return true;
    }

    public virtual int GetFinalValue()
    {
        float flatBonus = _bonusValues[BonusValueType.flat].bonusValue;
        float pctBonus = _bonusValues[BonusValueType.percentage].bonusValue;
        float multBonus = _bonusValues[BonusValueType.mult].bonusValue;

        float finalValue = _flatFirst ? (_currentValue + flatBonus) * pctBonus : _currentValue * pctBonus + flatBonus;

        return NormalizeValue(finalValue * multBonus);
    }

    public void ResetBonusValues()
    {
        _bonusValues.ForEach(bonusValue => bonusValue.Value.ResetValue());
    }

    public ValueState GetState()
    {
        int numericState = (int)Mathf.Sign(GetFinalValue() - _currentValue);
        return (ValueState)numericState;
    }

    protected int NormalizeValue(float value)
    {
        value = Mathf.Clamp(value, _minValue, _maxValue);
        return Mathf.RoundToInt(value);
    }

    private int GetSumSafe(int a, int b)
    {
        //prevent overflow
        if (b < 0 && int.MinValue - b > a)
        {
            return _minValue;
        }
        else if (b > 0 && int.MaxValue - b < a)
        {
            return _maxValue;
        }
        else
        {
            return a + b;
        }
    }
}
