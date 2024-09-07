using System.Collections.Generic;
using Entities.NPC;

namespace Items
{
    public interface ILootContainer
    {
        void AddItems(LootTable lootTable);
        void AddItems(ItemStorage storage);
        void RemoveItems(ItemStorage storage);
    }
}


