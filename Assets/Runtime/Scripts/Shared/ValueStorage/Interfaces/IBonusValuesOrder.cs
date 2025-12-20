using System.Collections.Generic;

public interface IBonusValuesOrder
{
    IEnumerable<BonusValueType> GetOrder();
}
