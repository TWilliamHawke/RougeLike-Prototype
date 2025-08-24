using System.Collections;
using System.Collections.Generic;
using Core;
using Core.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    [System.Serializable]
    public class ItemSlotData : IDataCount<Item>, IItemSlotDataUnsafe, IContextActionSource
    {
        [SerializeField] Item _item;
        [SerializeField] int _count;

        public int slotPrice { get; set; }
        
        public Item item => _item;
        public int count => _count;
        public event UnityAction OnSlotDataChanged;
        Item IDataCount<Item>.element => _item;

        public ItemSlotData()
        {
            
        }

        public ItemSlotData(Item item, int count, int slotPrice = -1)
        {
            _item = item;
            _count = count;
            this.slotPrice = slotPrice;
        }

        public void AddOneItem()
        {
            _count++;
            OnSlotDataChanged?.Invoke();
        }

        public void RemoveOneItem()
        {
            _count--;
            OnSlotDataChanged?.Invoke();
        }

        public void RemoveAllItems()
        {
            _count = 0;
            OnSlotDataChanged?.Invoke();
        }

        public void FillToMaxSize()
        {
            _count = item.maxStackSize;
            OnSlotDataChanged?.Invoke();
        }

        public IEnumerable<ContextActionTemplate> GetActions()
        {
            yield break;
        }

        void IItemSlotDataUnsafe.IncreaseCountBy(int num)
        {
            _count += num;
        }

        void IItemSlotDataUnsafe.DecreaseCountBy(int num)
        {
            _count = Mathf.Max(0, _count - num);
            OnSlotDataChanged?.Invoke();
        }

        void IItemSlotDataUnsafe.SetCount(int count)
        {
            _count = count;
        }
    }
}