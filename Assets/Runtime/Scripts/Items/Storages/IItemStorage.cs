using System.Collections.Generic;
using Entities.NPC;

namespace Items
{
    public interface IItemStorage
    {
        void AddItems(LootTable lootTable);
        void AddItems(ItemContainer container);
        void RemoveItems(ItemContainer container);
    }
}


