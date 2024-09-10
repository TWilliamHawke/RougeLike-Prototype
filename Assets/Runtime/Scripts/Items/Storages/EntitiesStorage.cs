using System.Collections.Generic;
using Entities;
using System.Linq;

namespace Items
{
    public abstract class EntitiesStorage : IItemStorage, IContainersList, IObserver<Entity>
    {
        protected LootContainer _creaturesLoot = new();
        protected List<ItemContainer> _NPCItems = new();

        public int count => _NPCItems.Count + (_creaturesLoot.isEmpty ? 0 : 1);

        public ItemContainer this[int idx] => throw new System.NotImplementedException();

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

        public IEnumerable<ItemContainer> GetAllContainers()
        {
            if (!_creaturesLoot.isEmpty)
            {
                yield return _creaturesLoot;
            }

            foreach (var section in _NPCItems)
            {
                yield return section;
            }
        }

        public ItemContainer ContainerAt(int idx)
        {
            if (!_creaturesLoot.isEmpty)
            {
                if (idx == 0)
                {
                    return _creaturesLoot;
                }
                idx--;
            }

            return _NPCItems[idx];
        }

        public bool IsEmpty()
        {
            if (!_creaturesLoot.isEmpty) return false;

            return _NPCItems.All(x => x.isEmpty);
        }
    }
}


