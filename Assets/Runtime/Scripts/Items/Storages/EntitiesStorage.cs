using System.Collections.Generic;
using Entities;
using System.Linq;

namespace Items
{
    public abstract class EntitiesStorage : IItemStorage, IInteractiveStorage, IObserver<Entity>
    {
        protected LootContainer _lootItems = new();
        protected List<ItemContainer> _containers = new();

        public virtual int count => _containers.Count + (_lootItems.isEmpty ? 0 : 1);

        public abstract bool isStealingTarget { get; set; }
        public abstract void AddToObserve(Entity target);
        public abstract void RemoveFromObserve(Entity target);
        public abstract ItemContainer ContainerAt(int idx);
        protected abstract void HandleDeath(Entity target);

        public void AddItemsFrom(ItemContainer container)
        {
            _containers.Add(container);
        }

        public void AddItemsFrom(LootTable lootTable)
        {
            _lootItems.AddItemsFrom(lootTable);
        }

        public void AddItemsFrom(IEnumerable<ItemSlotData> items)
        {
            _lootItems.AddItemsFrom(items);
        }

        public void RemoveItems(ItemContainer container)
        {
            _containers.Remove(container);
        }

        public void RemoveItems(IEnumerable<ItemSlotData> items)
        {
            _lootItems.RemoveItems(items);
        }

        public IEnumerable<ItemContainer> GetAllContainers()
        {
            if (!_lootItems.isEmpty)
            {
                yield return _lootItems;
            }

            foreach (var container in _containers)
            {
                yield return container;
            }
        }

        public bool IsEmpty()
        {
            if (!_lootItems.isEmpty) return false;

            return _containers.All(x => x.isEmpty);
        }
    }
}


