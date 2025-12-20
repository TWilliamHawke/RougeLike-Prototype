using System.Collections.Generic;

public class DefaultBonusValuesOrder : IBonusValuesOrder
{
    public IEnumerable<BonusValueType> GetOrder()
    {
        yield return BonusValueType.flat;
        yield return BonusValueType.percentage;
        yield return BonusValueType.mult;
    }
}
