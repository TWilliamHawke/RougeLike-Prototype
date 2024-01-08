using Core;

namespace Items.Actions
{
    public abstract class ItemActionsFactory : IActionFactory<ItemSlotData>
    {
		protected abstract IRadialMenuAction CreateAction(ItemSlotData itemSlot);
		protected abstract bool SlotIsValid(ItemSlotData itemSlot);

        public bool TryCreateAction(ItemSlotData itemSlot, out IRadialMenuAction action)
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


