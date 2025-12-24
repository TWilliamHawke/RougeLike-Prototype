using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items.Equipment
{
    [Serializable]
    public class EquipmentStorage
    {
        static readonly int _size = Enum.GetValues(typeof(EquipmentTypes)).Length;
        [SerializeField] List<ItemSlotData> _equipment;

        public EquipmentStorage()
        {
            _equipment = new(_size);
            for (int i = 0; i < _size; i++)
            {
                _equipment.Add(null);
            }
        }

        public ItemSlotData GetEquipment(IEquipmentSlotTemplate slot)
        {
            return _equipment[slot.index];
        }

        public void AddEquipment(IEquipmentSlotTemplate slot, ItemSlotData item)
        {
            _equipment[slot.index] = item;
        }

        public void RemoveEquipment(IEquipmentSlotTemplate slot)
        {
            _equipment[slot.index] = null;
        }

        public bool TryRemoveEquipment(IEquipmentSlotTemplate slot, out ItemSlotData item)
        {
            item = _equipment[slot.index];
            _equipment[slot.index] = null;
            return item != null;
        }

        public bool HasEquipment(IEquipmentSlotTemplate slot)
        {
            if (slot.index == 0) return false;
            var slotContent = _equipment[slot.index];
            return slotContent != null && slotContent.item != null;
        }

        public void Clear()
        {
            _equipment.Clear();
        }

        public IEnumerable<ItemSlotData> GetItems()
        {
            //skip EquipmentTypes.none
            for (int i = 1; i < _size; i++)
            {
                if (_equipment[i] == null) continue;
                yield return _equipment[i];
            }
        }

    }

}