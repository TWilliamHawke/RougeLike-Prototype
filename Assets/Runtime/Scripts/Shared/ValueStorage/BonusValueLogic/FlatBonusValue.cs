public class FlatBonusValue : IBonusValueLogic
{
    const float BASE_VALUE = 0f;

    public float bonusValue { get; private set; } = BASE_VALUE;

    public void AddBonusValue(float value)
    {
        bonusValue += value;
    }

    public void RemoveBonusValue(float value)
    {
        bonusValue -= value;
    }

    public void ResetValue()
    {
        bonusValue = BASE_VALUE;
    }
}
