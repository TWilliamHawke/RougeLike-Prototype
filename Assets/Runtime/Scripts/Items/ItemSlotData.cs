using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [System.Serializable]
    public class ItemSlotData : IDataCount<Item>
    {
        [SerializeField] Item _item;
        [SerializeField] int _count = 1;

        public ItemSlotData(Item item)
        {
            _item = item;
        }

        public ItemSlotData(Item item, int count)
        {
            _item = item;
            _count = count;
        }

        public Item item => _item;
        public int count => _count;

        public void AddToStack()
        {
            _count++;
        }

        public void RemoveFromStack()
        {
            if (count == 0) return;
            _count--;
        }

        public void IncreaseCountBy(int num)
        {
            _count += num;
        }

        public void DecreaseCountBy(int num)
        {
            _count = Mathf.Max(0, _count - num);
        }

        public void FillToMaxSize()
        {
            _count = item.maxStackSize;
        }


    }
}