using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items.Equipment
{
    public class PlayerEquipment : ScriptableObject, IEquipmentController, IEquipmentStorage
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] CustomEvent _onEquipmentChanged;
        [SerializeField] ItemTemplate[] _testEquipment;

        EquipmentStorage _equipmentStorage = new();

        void OnEnable()
        {
            _equipmentStorage = new();

            foreach (var equipment in _testEquipment)
            {
                ItemSlotData slot = new(equipment.CreateItem(30));
                _equipmentStorage.AddEquipment(slot);
            }
        }

        public ItemSlotData GetEquipment(IEquipmentSlotTemplate slot)
        {
            return _equipmentStorage.GetEquipment(slot);
        }

        public void Equip(ItemSlotData itemSlotData)
        {
            var slot = itemSlotData.GetEquipmentSlot();
            if (slot.index == 0) return;
            _equipmentStorage.AddEquipment(itemSlotData.Clone());
            itemSlotData.RemoveOneItem();
            _onEquipmentChanged.Invoke();
        }

        public void Unequip(IEquipmentSlotTemplate slot)
        {
            if (_equipmentStorage.TryRemoveEquipment(slot, out var itemSlotData))
            {
                itemSlotData.RemoveAllItems();
                _inventory.AddItem(itemSlotData.item);
                _onEquipmentChanged.Invoke();
            }
        }

        public bool HasEquipment(IEquipmentSlotTemplate slot)
        {
            return _equipmentStorage.HasEquipment(slot);
        }

        public IEnumerable<ItemSlotData> GetAllItems()
        {
            return _equipmentStorage.GetItems();
        }
    }
}