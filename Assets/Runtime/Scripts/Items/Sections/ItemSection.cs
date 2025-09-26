using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace Items
{
    [System.Serializable]
    public class ItemSection : IUISectionData<ItemSlotData>, IItemSection, ILootStorage
    {

        List<ItemSlotData> _itemsList;
        Dictionary<IItem, ItemSlotData> _activeSlotsByItem;
        IItemSectionTemplate _itemSectionTemplate;
        string _sectionName;

        int maxSlotsCount => _itemSectionTemplate.startCapacity;
        int _sectionSize => maxSlotsCount >= 0 ? maxSlotsCount : 10;

        public event UnityAction OnSectionDataChange;

        public int filledSlotsCount => _itemsList.Count;
        public string sectionName => _sectionName;

        public ItemStorageType itemStorage => _itemSectionTemplate.storageType;
        public int capacity => _itemSectionTemplate.startCapacity;
        public bool isEmpty => _itemsList.Count == 0;

        public ItemSection(string sectionName = "Default")
        {
            _sectionName = sectionName;
            _itemSectionTemplate = new DefaultSection();
            _activeSlotsByItem = new Dictionary<IItem, ItemSlotData>(_sectionSize);
            _itemsList = new List<ItemSlotData>(_sectionSize);
        }

        public ItemSection(IItemSectionTemplate template)
        {
            _sectionName = template.sectionName;
            _itemSectionTemplate = template;
            _activeSlotsByItem = new Dictionary<IItem, ItemSlotData>(_sectionSize);
            _itemsList = new List<ItemSlotData>(_sectionSize);
        }

        public virtual bool ItemMeet(IItem item)
        {
            //item doesnt fit a section
            if (!_itemSectionTemplate.ItemTypeIsMeet(item)) return false;

            //inventory section has unlimited slots
            if (maxSlotsCount < 1) return true;

            //section has empty slot
            if (_itemsList.Count < maxSlotsCount) return true;

            //no empty slots - check exist slots
            //slot with item doesnt exist
            if (!_activeSlotsByItem.ContainsKey(item)) return false;

            //slot with item is exist - check empty space
            if (_activeSlotsByItem[item].count < item.maxStackSize) return true;

            //no empty space in section
            return false;
        }

        public void AddItem(IItem someItem)
        {
            AddItems(someItem, 1);
        }

        public void Clear()
        {
            _itemsList.Clear();
            _activeSlotsByItem.Clear();
        }

        public void AddItems(ItemSlotData itemSlotData)
        {
            AddItems(itemSlotData.item, itemSlotData.count);
        }

        //UNDONE this code can create slots over _maxSlotsCount!!!
        //should return int
        public void AddItems(IItem item, int count)
        {            
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

        public int FindItemCount(IItem item)
        {
            int itemsCount = 0;
            foreach (var slot in _itemsList)
            {
                if(slot.item != item) continue;
                itemsCount += slot.count;
            }

            return itemsCount;
        }

        public void AddItemsFrom(LootTable lootTable)
        {
            lootTable.FillItemSection(this);
        }

        public bool HasItem(IItem item)
        {
            return _activeSlotsByItem.ContainsKey(item);
        }

        public void RemoveItem(IItem item)
        {
            if (!_activeSlotsByItem.ContainsKey(item)) return;
            var itemSlot = _activeSlotsByItem[item];
            itemSlot.RemoveOneItem();
        }

        private void Refresh()
        {
            _activeSlotsByItem = _activeSlotsByItem
                .Where(pair => pair.Value.count > 0)
                .ToDictionary(pair => pair.Key, pair => pair.Value);

            _itemsList = _itemsList
                .Where(slotData => slotData.count > 0)
                .ToList();

            OnSectionDataChange?.Invoke();
        }

        private void CreateNewItemSlot(IItem item, int count)
        {
            var itemSlotData = new ItemSlotData(item, count);
            itemSlotData.OnSlotDataChanged += Refresh;
            _activeSlotsByItem[item] = itemSlotData;
            _itemsList.Add(itemSlotData);
        }

        public IEnumerator<ItemSlotData> GetEnumerator()
        {
            for (int i = 0; i < _itemsList.Count; i++)
            {
                yield return _itemsList[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

}