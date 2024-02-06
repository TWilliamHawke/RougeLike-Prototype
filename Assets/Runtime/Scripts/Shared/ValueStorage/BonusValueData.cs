public struct BonusValueData
{
    public float value { get; init; }
    public BonusValueType valueBonusType { get; init; }
    
    public BonusValueData(float value, BonusValueType valueBonusType)
    {
        this.value = value;
        this.valueBonusType = valueBonusType;
    }

}