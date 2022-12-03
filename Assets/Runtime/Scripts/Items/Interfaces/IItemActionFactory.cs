using Core;

namespace Items
{
	public interface IItemActionFactory
	{
		bool TryCreateItemAction(ItemSlot itemSlot, out IContextAction action);
	}
}


