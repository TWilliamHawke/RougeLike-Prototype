namespace Items
{
    public class EquipmentSlotData : ItemSlotData
	{
		EquipmentTypes _equipmentType;

		public EquipmentSlotData(ItemSlotData itemSlotData) : base(itemSlotData.item, itemSlotData.count)
		{
			_equipmentType = (itemSlotData.item as IEquipment).equipmentType;
		}
	}
}