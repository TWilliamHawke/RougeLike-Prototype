namespace Items.Equipment
{
    public interface IEquipmentSelectior
    {
        void ShowEquipmentInSection(IEquipmentSlotTemplate slotTemplate, IInventoryIterator iterator);
    }
}