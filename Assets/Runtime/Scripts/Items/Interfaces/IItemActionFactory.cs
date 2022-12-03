using Core;

namespace Items
{
	public interface IItemActionFactory
	{
		bool TryCreateItemAction(IItemSlot itemSlot, out IRadialMenuAction action);
	}
}


