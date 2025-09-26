using System.Collections.Generic;

namespace Items
{
    public interface ILootStorage: IEnumerable<ItemSlotData>
    {
        void AddItem(IItem someItem);
        bool isEmpty { get; }
        void Clear();
    }
}