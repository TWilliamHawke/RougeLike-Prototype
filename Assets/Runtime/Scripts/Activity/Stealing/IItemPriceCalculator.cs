using System.Collections.Generic;

namespace Items
{
    public interface IItemPriceCalculator
    {
        void SetPrices(IEnumerable<IItemStorage> inventory);
    }
}
