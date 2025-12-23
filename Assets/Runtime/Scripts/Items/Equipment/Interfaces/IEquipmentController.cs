namespace Items
{
    public interface IEquipmentController
    {
        void Equip(ItemSlotData itemSlotData);
        void Unequip(EquipmentTypes type);
    }
}