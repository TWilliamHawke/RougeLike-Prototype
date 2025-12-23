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

        public ItemSlotData GetEquipment(EquipmentTypes type)
        {
            return _equipment[(int)type];
        }

        public void AddEquipment(EquipmentTypes type, ItemSlotData item)
        {
            _equipment[(int)type] = item;
        }

        public void RemoveEquipment(EquipmentTypes type)
        {
            _equipment[(int)type] = null;
        }

        public bool TryRemoveEquipment(EquipmentTypes type, out ItemSlotData item)
        {
            item = _equipment[(int)type];
            _equipment[(int)type] = null;
            return item != null;
        }

        public bool HasEquipment(EquipmentTypes type)
        {
            if (type == EquipmentTypes.none) return false;
            var slotContent = _equipment[(int)type];
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
                yield return _equipment[i];
            }
        }

    }

}