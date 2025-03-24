public interface IBonusValueLogic
{
    float bonusValue { get; }
    void AddBonusValue(float value);
    void RemoveBonusValue(float value);
    void ResetValue();
}
