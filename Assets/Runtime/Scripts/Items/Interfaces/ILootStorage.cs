using System.Collections.Generic;

namespace Items
{
    public interface ILootStorage: IDataList<IItem>, IEnumerable<ItemSlotData>
    {
        bool isEmpty { get; }
        void Clear();
    }
}