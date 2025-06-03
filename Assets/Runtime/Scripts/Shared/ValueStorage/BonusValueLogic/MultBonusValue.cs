public class MultBonusValue : IBonusValueLogic
{
    const float BASE_VALUE = 1f;

    public float bonusValue { get; private set; } = BASE_VALUE;

    public void AddBonusValue(float value)
    {
        value = value >= 10 ? value / 100 : value;
        bonusValue *= value;
    }

    public void RemoveBonusValue(float value)
    {
        value = value >= 10 ? value / 100 : value;
        bonusValue /= value;
    }

    public void ResetValue()
    {
        bonusValue = BASE_VALUE;
    }
}