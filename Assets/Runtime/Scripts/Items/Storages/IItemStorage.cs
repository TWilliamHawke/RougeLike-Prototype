using System.Collections;
using System.Collections.Generic;
using Entities.NPC;

namespace Items
{
    public interface IItemStorage
    {
        void AddItemsFrom(LootTable lootTable);
        void AddItemsFrom(ItemContainer container);
        void AddItemsFrom(IEnumerable<ItemSlotData> items);
        void RemoveItems(ItemContainer container);
        void RemoveItems(IEnumerable<ItemSlotData> items);
    }
}


