using System.Collections.Generic;

namespace Items
{
    public interface ILootStorage: IDataList<Item>, IEnumerable<ItemSlotData>
    {

    }
}