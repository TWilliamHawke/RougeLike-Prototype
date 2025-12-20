public class FlatBonusValue : IBonusValueLogic
{
    const float BASE_VALUE = 0f;

    float _bonusValue = BASE_VALUE;

    public void AddBonusValue(float value)
    {
        _bonusValue += value;
    }

    public float ApplyBonus(float value)
    {
        return value + _bonusValue;
    }

    public void RemoveBonusValue(float value)
    {
        _bonusValue -= value;
    }

    public void ResetValue()
    {
        _bonusValue = BASE_VALUE;
    }
}
