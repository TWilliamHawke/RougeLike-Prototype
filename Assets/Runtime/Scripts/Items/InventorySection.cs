using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [System.Serializable]
    public class InventorySection<T> : IInventorySection, IEnumerable<ItemSlotData<T>> where T : Item
    {
        List<ItemSlotData<T>> _itemsList;
        Dictionary<T, ItemSlotData<T>> _items;

        int _maxSlotsCount;

        public ItemSlotData<T> this[int idx] => _itemsList[idx];
        public int count => _itemsList.Count;

        public InventorySection(int maxSlotsCount)
        {
            _maxSlotsCount = maxSlotsCount;
            int listSize = _maxSlotsCount > 0 ? _maxSlotsCount : 10;
            _items = new Dictionary<T, ItemSlotData<T>>(listSize);
            _itemsList = new List<ItemSlotData<T>>(listSize);
        }


        public bool ItemMeet(Item someItem)
        {
            T item = someItem as T;

            //item doesnt fit a section
            if (item == null) return false;

            //inventory section has unlimited slots
            if (_maxSlotsCount < 1) return true;

            //section has empty slot
            if (_itemsList.Count < _maxSlotsCount) return true;

            //no empty slots - check exist slots
            //slot with item doesnt exist
            if (!_items.ContainsKey(item)) return false;

            //slot with item is exist - check empty space
            if (_items[item].count < item.maxStackSize) return true;

            //no empty space in section
            return false;
        }

        public void AddItem(Item someItem)
        {
            AddItems(someItem, 1);
        }

        //UNDONE this code can create slots over _maxSlotsCount!!!
        //should return int
        public void AddItems(Item someItem, int count)
        {
            if (!someItem is T)
            {
                Debug.Log("Wrong item type");
                return;
            }

            var item = someItem as T;

            if (_items.ContainsKey(item))
            {
                int freeSpace = item.maxStackSize - _items[item].count;
                if (freeSpace < count)
                {
                    count -= freeSpace;
                    CreateNewItemSlot(item, count);
                }
                else
                {
                    _items[item].IncreaseCountBy(count);
                }
            }
            else
            {
                CreateNewItemSlot(item, count);
            }
        }

        public void Clear()
        {
            _itemsList.Clear();
            _items.Clear();
        }

        public IEnumerator<ItemSlotData<T>> GetEnumerator()
        {
            return _itemsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _itemsList.GetEnumerator();
        }

        void CreateNewItemSlot(T item, int count)
        {
            var itemSlotData = new ItemSlotData<T>(item, count);
            _items[item] = itemSlotData;
            _itemsList.Add(itemSlotData);
        }


    }

}