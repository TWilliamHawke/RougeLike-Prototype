using UnityEngine;

namespace Items
{
    public class PlayerEquipment : ScriptableObject, IEquipmentController, IEquipmentStorage
    {
        [SerializeField] Inventory _inventory;

        EquipmentStorage _equipmentStorage = new();

        void OnEnable()
        {
            _equipmentStorage = new();
        }

        public ItemSlotData GetEquipment(EquipmentTypes type)
        {
            return _equipmentStorage.GetEquipment(type);
        }

        public void Equip(ItemSlotData itemSlotData)
        {
            var slot = itemSlotData.GetEquipmentSlot();
            if (slot == EquipmentTypes.none) return;
            _equipmentStorage.AddEquipment(slot, itemSlotData.Clone());
            itemSlotData.RemoveOneItem();
        }

        public void Unequip(EquipmentTypes type)
        {
            if (_equipmentStorage.TryRemoveEquipment(type, out var itemSlotData))
            {
                _inventory.AddItem(itemSlotData.item);
            }
        }

        public bool HasEquipment(EquipmentTypes type)
        {
            return _equipmentStorage.HasEquipment(type);
        }
    }
}