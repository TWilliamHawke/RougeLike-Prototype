public interface IBonusValueLogic
{
    float bonusValue { get; }
    void AddBonusValue(float value);
    void ResetValue();
}
