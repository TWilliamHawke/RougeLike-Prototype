namespace Items.Equipment
{
    public interface IEquipmentStorage
	{
        ItemSlotData GetEquipment(EquipmentTypes type);
		bool HasEquipment(EquipmentTypes type);
	}
}