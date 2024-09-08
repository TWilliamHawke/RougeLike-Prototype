using System.Collections.Generic;
using Entities;

namespace Items
{
    public abstract class EntitiesStorage : ItemStorage, IObserver<Entity>
    {
        LootContainer _creaturesLoot = new();
        List<ItemContainer> _NPCItems = new();

        public abstract void AddToObserve(Entity target);
        public abstract void RemoveFromObserve(Entity target);
        protected abstract void HandleDeath(Entity target);

        public void AddItems(ItemContainer container)
        {
            _NPCItems.Add(container);
        }

        public void AddItems(LootTable lootTable)
        {
            _creaturesLoot.AddItemsFrom(lootTable);
        }

        public void RemoveItems(ItemContainer container)
        {
            _NPCItems.Remove(container);
        }
    }
}


