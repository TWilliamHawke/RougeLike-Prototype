using System.Collections.Generic;
using Entities;

namespace Items
{
    public abstract class EntitiesInventory : ILootContainer, IObserver<Entity>
    {
        LootStorage _creaturesLoot = new();
        List<ItemStorage> _NPCItems = new();

        public abstract void AddToObserve(Entity target);
        public abstract void RemoveFromObserve(Entity target);
        protected abstract void HandleDeath(Entity target);

        public void AddItems(ItemStorage storage)
        {
            _NPCItems.Add(storage);
        }

        public void AddItems(LootTable lootTable)
        {
            _creaturesLoot.AddItemsFrom(lootTable);
        }

        public void RemoveItems(ItemStorage storage)
        {
            _NPCItems.Remove(storage);
        }
    }
}


