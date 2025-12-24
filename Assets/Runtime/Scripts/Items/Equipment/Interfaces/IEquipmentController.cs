namespace Items.Equipment
{
    public interface IEquipmentController
    {
        void Equip(ItemSlotData itemSlotData);
        void Unequip(IEquipmentSlotTemplate type);
    }
}