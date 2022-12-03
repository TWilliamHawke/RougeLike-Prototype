using Core;

namespace Items.Actions
{
    public class Buy : ItemActionsFactory
    {
        protected override IRadialMenuAction CreateAction(IItemSlot itemSlot)
        {
            return new BuyAction(itemSlot);
        }

        protected override bool SlotIsValid(IItemSlot itemSlot)
        {
            return itemSlot.itemSlotContainer == ItemSlotContainers.trader;
        }

        class BuyAction : IItemAction
        {
            public string actionTitle => "Buy";
            public RadialButtonPosition preferedPosition => RadialButtonPosition.top;
            
            IItemSlot _itemSlot;

            public BuyAction(IItemSlot itemSlot)
            {
                _itemSlot = itemSlot;
            }
        }
    }
}