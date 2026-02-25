using UnityEngine;

public class PercentageBonusValue : IBonusValueLogic
{
    const float BASE_VALUE = 0f;
    const float MIN_VALUE = -100f;

    float _bonusValue = BASE_VALUE;

    public float ApplyBonus(float value)
    {
        return value * (1f + (NormalizeValue(_bonusValue) / 100f));
    }

    public void AddBonusValue(float value)
    {
        _bonusValue = _bonusValue + value;
    }

    public void RemoveBonusValue(float value)
    {
        _bonusValue = _bonusValue - value;
    }

    public void ResetValue()
    {
        _bonusValue = BASE_VALUE;
    }

    private float NormalizeValue(float value)
    {
        return Mathf.Max(value, MIN_VALUE);
    }
}
