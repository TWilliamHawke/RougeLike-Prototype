using System.Collections.Generic;

namespace Items
{
    public interface ILootStorage
    {
        IEnumerable<ItemSlotData> GetItems();
        void AddItems(IItem someItem, int count);
        bool isEmpty { get; }
        void Clear();
    }
}