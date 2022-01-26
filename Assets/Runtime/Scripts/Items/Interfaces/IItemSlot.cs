namespace Items
{
	public interface IItemSlot
	{
	    ItemSlotContainers itemSlotContainer { get; }
		ItemSlotData itemSlotData { get; }
	}
}