using Core;

namespace Items
{
	public interface IItemActionFactory
	{
		bool TryCreateItemAction(ItemSlotData itemSlot, out IRadialMenuAction action);
	}
}


