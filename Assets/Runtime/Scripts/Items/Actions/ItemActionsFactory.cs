using Core;

namespace Items.Actions
{
    public abstract class ItemActionsFactory : IItemActionFactory
    {
		protected abstract IRadialMenuAction CreateAction(IItemSlot itemSlot);
		protected abstract bool SlotIsValid(IItemSlot itemSlot);

        public bool TryCreateItemAction(IItemSlot itemSlot, out IRadialMenuAction action)
        {
            action = default;
            if (SlotIsValid(itemSlot))
            {
                action = CreateAction(itemSlot);
                return true;
            }
            return false;
        }
    }
}


