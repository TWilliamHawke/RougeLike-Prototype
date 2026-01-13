using System.Collections.Generic;

namespace Items
{
    public interface ILootSection
    {
        IEnumerable<ItemSlotData> GetItems();
        void AddItems(IItem someItem, int count);
        bool isEmpty { get; }
        void Clear();
    }
}