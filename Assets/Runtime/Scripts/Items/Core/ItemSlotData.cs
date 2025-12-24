using System.Collections;
using System.Collections.Generic;
using Core;
using Core.UI;
using Items.Equipment;
using UnityEngine;
using UnityEngine.Events;

namespace Items
{
    [System.Serializable]
    public class ItemSlotData : IDataCount<IItem>, IItemSlotDataUnsafe
    {
        //[SerializeField] requires for show inventory in inspector
        [SerializeField] IItem _item;
        [SerializeField] int _count;

        public int slotPrice { get; set; }
        
        public IItem item => _item;
        public int count => _count;
        public event UnityAction OnSlotDataChanged;
        IItem IDataCount<IItem>.element => _item;

        public ItemSlotData()
        {
            
        }

        public ItemSlotData(IItem item, int count, int slotPrice = -1)
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

        public IEquipmentSlotTemplate GetEquipmentSlot()
        {
            if (item is IEquipment equipment)
            {
                return equipment.equipmentSlot;
            }
            return new DummyEquipmentSlot(_item);
        }

        public ItemSlotData Clone()
        {
            return new ItemSlotData(_item, _count, slotPrice);
        }

        public IEnumerable<ContextActionTemplate> GetActions(ItemSectionTemplate section)
        {
            foreach (var action in section.GetActions())
            {
                yield return action;
            }

            foreach (var action in item.GetActions())
            {
                yield return action;
            }
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