namespace Items.Equipment
{
    public interface IEquipmentSelectior
    {
        void ShowMainItems(EquipmentSlotTemplate slotTemplate);
        void ShowStorageItems(EquipmentSlotTemplate slotTemplate);
    }
}