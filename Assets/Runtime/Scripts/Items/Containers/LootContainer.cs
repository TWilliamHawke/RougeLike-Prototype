using System.Collections.Generic;

namespace Items
{
    public class LootContainer : ItemContainer, IContainersList
    {
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

        public void AddItems(IEnumerable<ItemSlotData> itemSlots)
        {
            itemSlots.ForEach(itemSlot => _itemsSection.AddItems(itemSlot));
        }

        public ItemContainer ContainerAt(int idx)
        {
            return this;
        }

        public IEnumerable<ItemContainer> GetAllContainers()
        {
            yield return this;
        }

        public bool IsEmpty()
        {
            return isEmpty;
        }
    }
}


