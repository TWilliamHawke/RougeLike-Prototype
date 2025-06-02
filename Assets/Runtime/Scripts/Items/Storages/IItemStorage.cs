using System.Collections.Generic;
using Entities.NPC;

namespace Items
{
    public interface IItemStorage
    {
        void AddItemsFrom(LootTable lootTable);
        void AddItemsFrom(ItemContainer container);
        void RemoveItems(ItemContainer container);
    }
}


