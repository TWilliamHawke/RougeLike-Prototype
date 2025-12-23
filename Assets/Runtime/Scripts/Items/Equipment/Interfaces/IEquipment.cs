namespace Items
{
    public interface IEquipment
	{
		EquipmentTypes equipmentType { get; }
	}

	public interface IEquipmentStorage
	{
        ItemSlotData GetEquipment(EquipmentTypes type);
		bool HasEquipment(EquipmentTypes type);
	}
}