public class MultBonusValue : IBonusValueLogic
{
    const float BASE_VALUE = 1f;

    float _bonusValue = BASE_VALUE;

    public void AddBonusValue(float value)
    {
        value = value >= 10 ? value / 100 : value;
        _bonusValue *= value;
    }

    public float ApplyBonus(float value)
    {
        return value * _bonusValue;
    }

    public void RemoveBonusValue(float value)
    {
        value = value >= 10 ? value / 100 : value;
        _bonusValue /= value;
    }

    public void ResetValue()
    {
        _bonusValue = BASE_VALUE;
    }
}