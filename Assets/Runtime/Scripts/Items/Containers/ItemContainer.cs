using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Items;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    public interface IItemContainer : IEnumerable<ItemSlotData>
    {
        ItemStorageType storageType { get; }
    }

    public class ItemContainer : IItemContainer, IInventorySectionData
    {
        public int lockLevel { get; private set; } = 0;
        public int trapLevel { get; private set; } = 0;
        public string storageName { get; private set; }
        public bool isIdentified { get; private set; } = false;

        protected ItemSection _itemsSection;

        public event UnityAction OnSectionDataChange;

        public virtual ItemStorageType storageType => _itemsSection.itemStorage;
        public int capacity => _itemsSection.capacity;
        public int filledSlotsCount => _itemsSection.filledSlotsCount;
        public bool isEmpty => _itemsSection.isEmpty;
        public bool isInfinity => _itemsSection.isInfinity;
        public string sectionName => storageName;

        public ItemContainer(string name, LootTable lootTable)
        {
            _itemsSection = new ItemSection(name);
            _itemsSection.OnSectionDataChange += HandleSectionChangeEvent;
            storageName = name;
            lootTable.FillItemSection(ref _itemsSection);
        }

        public ItemContainer(ItemContainerData template) : this(template.storageName, template.loot)
        {
        }

        protected ItemContainer()
        {
        }

        public void Unlock()
        {
            lockLevel = 0;
        }

        public void DisarmTrap()
        {
            trapLevel = 0;
        }

        private void HandleSectionChangeEvent()
        {
            OnSectionDataChange?.Invoke();
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


