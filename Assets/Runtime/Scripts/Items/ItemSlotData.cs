using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [System.Serializable]
    public class ItemSlotData : DataCount<Item>
    {
        public ItemSlotData(Item item) : base(item)
        {
        }

        public ItemSlotData(Item item, int count) : base(item, count)
        {
        }

        public void FillToMaxSize()
        {
            SetCount(item.maxStackSize);
        }


    }
}