using UnityEngine;

namespace Items.Equipment
{
    public class PlayerEquipment : ScriptableObject, IEquipmentController, IEquipmentStorage
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] CustomEvent _onEquipmentChanged;

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
            if (slot.equipmentType == EquipmentTypes.none) return;
            _equipmentStorage.AddEquipment(slot, itemSlotData.Clone());
            itemSlotData.RemoveOneItem();
            _onEquipmentChanged.Invoke();
        }

        public void Unequip(EquipmentSlotTemplate slot)
        {
            if (_equipmentStorage.TryRemoveEquipment(slot, out var itemSlotData))
            {
                _inventory.AddItem(itemSlotData.item);
                _onEquipmentChanged.Invoke();
            }
        }

        public bool HasEquipment(EquipmentTypes type)
        {
            return _equipmentStorage.HasEquipment(type);
        }
    }
}