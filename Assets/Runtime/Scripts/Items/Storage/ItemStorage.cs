using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items;
using UnityEngine;

namespace Items
{
    public interface IItemStorage : IEnumerable<ItemSlotData>
    {
        ItemStorageType storageType { get;}
    }
    
    public class ItemStorage : IItemStorage
    {
        public int lockLevel { get; private set; } = 0;
        public int trapLevel { get; private set; } = 0;
        public string storageName { get; init; }
        public bool isIdentified { get; private set; } = false;

        ItemSection<Item> _itemsInContainer;
        HashSet<ItemSlotData> _selectedItems = new();

        public IEnumerable<ItemSlotData> selectedItems => _selectedItems;

        public ItemStorageType storageType => _itemsInContainer.itemStorage;

        public ItemStorage(string name, LootTable items, ItemStorageType containerType)
        {
            _itemsInContainer = new ItemSection<Item>(containerType);
            storageName = name;
            items.FillItemSection(ref _itemsInContainer);
        }

        public ItemStorage(ItemStorageData template)
        {
            _itemsInContainer = new ItemSection<Item>(ItemStorageType.trader);
            template.loot.FillItemSection(ref _itemsInContainer);
            storageName = template.storageName;
        }

        public ItemStorage(ItemSection<Item> itemSection, string containerName)
        {
            this.storageName = containerName;
            _itemsInContainer = itemSection;
        }

        public IEnumerable<ItemSlotData> GetUnselectedItems()
        {
            return _itemsInContainer.Except(_selectedItems);
        }

        public void SelectItem(ItemSlotData item)
        {
            if(!_itemsInContainer.Contains(item)) return;
            _selectedItems.Add(item);
        }

        public void DeselectItem(ItemSlotData item)
        {
            _selectedItems.Remove(item);
        }

        public void Unlock()
        {
            lockLevel = 0;
        }

		public void DisarmTrap()
		{
			trapLevel = 0;
		}

        public IEnumerator<ItemSlotData> GetEnumerator()
        {
            return _itemsInContainer.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _itemsInContainer.GetEnumerator();
        }
    }
}


