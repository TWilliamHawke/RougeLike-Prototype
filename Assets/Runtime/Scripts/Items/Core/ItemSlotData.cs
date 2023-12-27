using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [System.Serializable]
    public class ItemSlotData : IDataCount<Item>, IItemSlotDataUnsafe
    {
        [SerializeField] Item _item;
        [SerializeField] int _count;

        IItemSectionInfo _itemSection;
        public int slotPrice { get; set; }
        
        public ItemStorageType slotContainer => _itemSection.itemContainer;

        public ItemSlotData(Item item, int count,
            IItemSectionInfo itemSectionInfo, int slotPrice = -1)
        {
            _item = item;
            _count = count;
            _itemSection = itemSectionInfo;
            this.slotPrice = slotPrice;
        }

        public Item item => _item;
        public int count => _count;
        Item IDataCount<Item>.element => _item;

        public void AddOneItem()
        {
            _count++;
            _itemSection.Refresh();
        }

        public void RemoveOneItem()
        {
            _count--;
            _itemSection.Refresh();
        }

        public void RemoveAllItems()
        {
            _count = 0;
            _itemSection.Refresh();
        }

        public void FillToMaxSize()
        {
            _count = item.maxStackSize;
            _itemSection.Refresh();
        }

        void IItemSlotDataUnsafe.IncreaseCountBy(int num)
        {
            _count += num;
        }

        void IItemSlotDataUnsafe.DecreaseCountBy(int num)
        {
            _count = Mathf.Max(0, _count - num);
            _itemSection.Refresh();
        }

        void IItemSlotDataUnsafe.SetCount(int count)
        {
            _count = count;
        }
    }
}