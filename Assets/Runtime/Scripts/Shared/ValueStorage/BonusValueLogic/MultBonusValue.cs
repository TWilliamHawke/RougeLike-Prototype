public class MultBonusValue : IBonusValueLogic
{
    const float BASE_VALUE = 1f;

    float _bonusValue = BASE_VALUE;

    public float ApplyBonus(float value)
    {
        return value * _bonusValue;
    }

    public void AddBonusValue(float value)
    {
        value = AdjustBonusValue(value);
        _bonusValue *= value;
    }

    public void RemoveBonusValue(float value)
    {
        value = AdjustBonusValue(value);
        _bonusValue /= value;
    }

    public void ResetValue()
    {
        _bonusValue = BASE_VALUE;
    }

    private float AdjustBonusValue(float value)
    {
        return value >= 10 ? value / 100 : value;
    }
}