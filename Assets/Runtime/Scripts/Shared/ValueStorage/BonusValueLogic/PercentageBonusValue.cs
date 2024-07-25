public class PercentageBonusValue : IBonusValueLogic
{
    static float BASE_VALUE = 1f;

    public float bonusValue { get; private set; } = BASE_VALUE;

    public void AddBonusValue(float value)
    {
        bonusValue += value * 0.01f;
    }

    public void ResetValue()
    {
        bonusValue = BASE_VALUE;
    }
}
