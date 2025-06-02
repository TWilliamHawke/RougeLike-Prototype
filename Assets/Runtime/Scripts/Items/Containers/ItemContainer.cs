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
        public string storageName { get; init; }
        public bool isIdentified { get; private set; } = false;

        protected ItemSection _itemsSection;

        public event UnityAction OnSectionDataChange;

        public virtual ItemStorageType storageType => _itemsSection.itemStorage;
        public int capacity => _itemsSection.capacity;
        public int filledSlotsCount => _itemsSection.filledSlotsCount;
        public bool isEmpty => _itemsSection.isEmpty;
        public bool isInfinity => _itemsSection.isInfinity;
        public string sectionName => _itemsSection.sectionName;

        public ItemContainer(ItemSection itemsSection) : this(itemsSection.sectionName, itemsSection)
        {
        }

        public ItemContainer(string name, ItemSection itemsSection)
        {
            _itemsSection = itemsSection;
            _itemsSection.OnSectionDataChange += HandleSectionChangeEvent;
            storageName = name;
        }

        public ItemContainer(ItemContainerData template)
        {
            storageName = template.storageName;
            _itemsSection = new(template.storageName);
            _itemsSection.AddItemsFrom(template.loot);
            _itemsSection.OnSectionDataChange += HandleSectionChangeEvent;
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


