using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

namespace Items
{
    [System.Serializable]
    public class ItemSection<T> : IEnumerable<ItemSlotData>, IInventorySectionData, IItemSection where T : Item
    {

        List<ItemSlotData> _itemsList;
        Dictionary<Item, ItemSlotData> _items;

        int _maxSlotsCount;

        public event UnityAction OnItemAdd;
        public event UnityAction OnItemRemove;

        ItemSlotData IInventorySectionData.this[int idx] => _itemsList[idx];
        public int maxCount => _maxSlotsCount > 0 ? _maxSlotsCount : 10;
        int IInventorySectionData.count => _itemsList.Count;

        public ItemSection(int maxSlotsCount)
        {
            _maxSlotsCount = maxSlotsCount;
            _items = new Dictionary<Item, ItemSlotData>(maxCount);
            _itemsList = new List<ItemSlotData>(maxCount);
        }

        public void RemoveItemFromSlot(ItemSlotData itemSlotData)
        {
            if(!_itemsList.Contains(itemSlotData)) return;

            if(itemSlotData.count == 1)
            {
                _itemsList.Remove(itemSlotData);
            }
            else
            {
                itemSlotData.DecreaseCountBy(1);
            }

            OnItemRemove?.Invoke();
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

        public void InvokeEvent()
        {
            Debug.Log("invoke");
            OnItemRemove?.Invoke();
        }

        public void Clear()
        {
            _itemsList.Clear();
            _items.Clear();
        }

        //UNDONE this code can create slots over _maxSlotsCount!!!
        //should return int
        public void AddItems(Item item, int count)
        {
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

            OnItemAdd?.Invoke();
        }

        void CreateNewItemSlot(Item item, int count)
        {
            var itemSlotData = new ItemSlotData(item, count);
            _items[item] = itemSlotData;
            _itemsList.Add(itemSlotData);
        }

        public IEnumerator<ItemSlotData> GetEnumerator()
        {
            return _itemsList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _itemsList.GetEnumerator();
        }

        public int FindItemCount(Item item)
        {
            return _itemsList.Where(slot => slot.item == item).Sum(slot => slot.count);
        }
    }

}