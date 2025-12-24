namespace Items.Equipment
{
    public interface IEquipmentSelectior
    {
        void ShowMainItems(IEquipmentSlotTemplate slotTemplate);
        void ShowStorageItems(IEquipmentSlotTemplate slotTemplate);
    }
}