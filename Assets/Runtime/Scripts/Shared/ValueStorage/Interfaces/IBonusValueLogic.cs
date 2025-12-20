public interface IBonusValueLogic
{
    void AddBonusValue(float value);
    void RemoveBonusValue(float value);
    void ResetValue();
    float ApplyBonus(float value);
}
