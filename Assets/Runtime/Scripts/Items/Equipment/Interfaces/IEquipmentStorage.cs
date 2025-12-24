namespace Items.Equipment
{
    public interface IEquipmentStorage
	{
        ItemSlotData GetEquipment(IEquipmentSlotTemplate slot);
		bool HasEquipment(IEquipmentSlotTemplate slot);
	}
}