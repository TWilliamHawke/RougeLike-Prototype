using System.Collections.Generic;

namespace Items
{
    public class LootContainer : ItemContainer, IContainersList
    {
        public bool isEmpty => _itemsSection.isEmpty;
        public override ItemStorageType storageType => ItemStorageType.loot;
        public int count => 1;

        public LootContainer()
        {
            _itemsSection = new ItemSection(new LootSectionTemplate());
        }

        public void AddItemsFrom(LootTable lootTable)
        {
            lootTable.FillItemSection(ref _itemsSection);
        }

        public ItemContainer ContainerAt(int idx)
        {
            return this;
        }

        public IEnumerable<ItemContainer> GetAllContainers()
        {
            yield return this;
        }
    }
}


