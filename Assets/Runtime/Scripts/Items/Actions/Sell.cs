using Core;

namespace Items.Actions
{
    public class Sell : ItemActionsFactory
    {
        protected override IRadialMenuAction CreateAction(IItemSlot itemSlot)
        {
            return new SellAction(itemSlot);
        }

        protected override bool SlotIsValid(IItemSlot itemSlot)
        {
            return itemSlot.itemSlotContainer != ItemSlotContainers.trader && false;
        }

        class SellAction : IItemAction
        {
            public string actionTitle => "Cell";
            IItemSlot _itemSlot;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.bottom;

            public SellAction(IItemSlot itemSlot)
            {
                _itemSlot = itemSlot;
            }
        }
    }
}