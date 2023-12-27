using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace Items
{
    [System.Serializable]
    public class ItemSection<T> : IInventorySectionData, IItemSection, ILootStorage, IItemSectionInfo where T : Item
    {

        List<ItemSlotData> _itemsList;
        Dictionary<Item, ItemSlotData> _activeSlotsByItem;

        int _maxSlotsCount;

        public event UnityAction OnSectionDataChange;

        int IInventorySectionData.count => _itemsList.Count;
        ItemContainerType IItemSectionInfo.itemContainer => _slotContainer;
        public int capacity => _maxSlotsCount > 0 ? _maxSlotsCount : 10;
        public bool isEmpty => _itemsList.Count == 0;

        ItemContainerType _slotContainer;

        public ItemSection(ItemContainerType slotContainer, int maxSlotsCount = -1)
        {
            _slotContainer = slotContainer;
            _maxSlotsCount = maxSlotsCount;
            _activeSlotsByItem = new Dictionary<Item, ItemSlotData>(capacity);
            _itemsList = new List<ItemSlotData>(capacity);
        }

        bool IItemSection.ItemMeet(Item someItem)
        {
            T item = someItem as T;

            //item doesnt fit a section
            if (item is null) return false;

            //inventory section has unlimited slots
            if (_maxSlotsCount < 1) return true;

            //section has empty slot
            if (_itemsList.Count < _maxSlotsCount) return true;

            //no empty slots - check exist slots
            //slot with item doesnt exist
            if (!_activeSlotsByItem.ContainsKey(item)) return false;

            //slot with item is exist - check empty space
            if (_activeSlotsByItem[item].count < item.maxStackSize) return true;

            //no empty space in section
            return false;
        }

        public void AddItem(Item someItem)
        {
            AddItems(someItem, 1);
        }

        public void Clear()
        {
            _itemsList.Clear();
            _activeSlotsByItem.Clear();
        }

        //UNDONE this code can create slots over _maxSlotsCount!!!
        //should return int
        public void AddItems(Item item, int count)
        {
            if(item is null) return;
            
            if (_activeSlotsByItem.TryGetValue(item, out var itemSlot))
            {
                int freeSpace = item.maxStackSize - itemSlot.count;
                var unsafeItemSlot = itemSlot as IItemSlotDataUnsafe;

                if (freeSpace < count)
                {
                    count -= freeSpace;
                    unsafeItemSlot.IncreaseCountBy(freeSpace);
                    CreateNewItemSlot(item, count);
                }
                else
                {
                    unsafeItemSlot.IncreaseCountBy(count);
                }
            }
            else
            {
                CreateNewItemSlot(item, count);
            }

            OnSectionDataChange?.Invoke();
        }

        public int FindItemCount(Item item)
        {
            int itemsCount = 0;
            foreach (var slot in _itemsList)
            {
                if(slot.item != item) continue;
                itemsCount += slot.count;
            }

            return itemsCount;
        }

        public void Refresh()
        {
            _activeSlotsByItem =_activeSlotsByItem
                .Where(pair => pair.Value.count > 0)
                .ToDictionary(pair => pair.Key, pair => pair.Value);
            
            _itemsList = _itemsList
                .Where(slotData => slotData.count > 0)
                .ToList();

            OnSectionDataChange?.Invoke();
        }

        void CreateNewItemSlot(Item item, int count)
        {
            var itemSlotData = new ItemSlotData(item, count, this);
            _activeSlotsByItem[item] = itemSlotData;
            _itemsList.Add(itemSlotData);
        }

        public IEnumerator<ItemSlotData> GetEnumerator()
        {
            for (int i = 0; i < capacity; i++)
            {
                if (i < _itemsList.Count)
                {
                    yield return _itemsList[i];
                }
                else
                {
                    yield return new ItemSlotData(null, 0, this);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

}