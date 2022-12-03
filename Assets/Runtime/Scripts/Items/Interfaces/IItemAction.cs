using Core;

namespace Items
{
	public interface IItemAction: IRadialMenuAction
	{
		IItemSlot itemSlot { get; set; }
		bool SlotIsValid(IItemSlot itemSlot);
	}
}