using System.Collections.Generic;

namespace Items
{
    public class SelectedItemsContainer : ItemContainer, IContainersList
    {
        Dictionary<ItemSlotData, ItemContainer> _selectedItems = new();

        public SelectedItemsContainer()
        {
            _itemsSection = new ItemSection(new LootSectionTemplate());
        }

        public int count => 1;

        public void AddItems(ItemSlotData itemSlotData)
        {
            _selectedItems.Add(itemSlotData, this);
            _itemsSection.AddItems(itemSlotData);
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


