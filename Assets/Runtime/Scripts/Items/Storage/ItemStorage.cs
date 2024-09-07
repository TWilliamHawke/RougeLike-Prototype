using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items;
using UnityEngine;

namespace Items
{
    public interface IItemContainer : IEnumerable<ItemSlotData>
    {
        ItemStorageType storageType { get;}
    }
    
    public class ItemStorage : IItemContainer
    {
        public int lockLevel { get; private set; } = 0;
        public int trapLevel { get; private set; } = 0;
        public string storageName { get; private set; }
        public bool isIdentified { get; private set; } = false;

        protected ItemSection _itemsSection;
        HashSet<ItemSlotData> _selectedItems = new();

        public IEnumerable<ItemSlotData> selectedItems => _selectedItems;

        public virtual ItemStorageType storageType => _itemsSection.itemStorage;

        public ItemStorage(string name, LootTable lootTable)
        {
            _itemsSection = new ItemSection(name);
            storageName = name;
            lootTable.FillItemSection(ref _itemsSection);
        }

        public ItemStorage(ItemStorageData template)
        {
            _itemsSection = new ItemSection(template.storageName);
            template.loot.FillItemSection(ref _itemsSection);
            storageName = template.storageName;
        }

        public ItemStorage(ItemSection itemSection, string containerName)
        {
            this.storageName = containerName;
            _itemsSection = itemSection;
        }

        protected ItemStorage()
        {
        }

        public IEnumerable<ItemSlotData> GetUnselectedItems()
        {
            return _itemsSection.Except(_selectedItems);
        }

        public void SelectItem(ItemSlotData item)
        {
            if(!_itemsSection.Contains(item)) return;
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
            return _itemsSection.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _itemsSection.GetEnumerator();
        }
    }
}


