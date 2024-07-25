public class MultBonusValue : IBonusValueLogic
{
    static float BASE_VALUE = 1f;

    public float bonusValue { get; private set; } = BASE_VALUE;

    public void AddBonusValue(float value)
    {
        bonusValue *= value;
    }

    public void ResetValue()
    {
        bonusValue = BASE_VALUE;
    }
}