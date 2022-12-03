using Core;

namespace Items.Actions
{
    public class Drop : ItemActionsFactory
    {
        protected override IRadialMenuAction CreateAction(IItemSlot itemSlot)
        {
            return new DropAction(itemSlot);
        }

        protected override bool SlotIsValid(IItemSlot itemSlot)
        {
            return itemSlot.itemSlotContainer == ItemSlotContainers.inventory ||
                itemSlot.itemSlotContainer == ItemSlotContainers.storage;
        }

        class DropAction : IItemAction
        {
            public string actionTitle => "Drop";
            IItemSlot _itemSlot;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.bottomRight;

            public DropAction(IItemSlot itemSlot)
            {
                _itemSlot = itemSlot;
            }
        }
    }
}