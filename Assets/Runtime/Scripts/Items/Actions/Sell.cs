using Core;

namespace Items.Actions
{
    public class Sell : ItemActionsFactory
    {
        protected override IRadialMenuAction CreateAction(ItemSlotData itemSlot)
        {
            return new SellAction(itemSlot);
        }

        protected override bool SlotIsValid(ItemSlotData itemSlot)
        {
            return itemSlot.slotContainer != ItemStorageType.trader && false;
        }

        class SellAction : IItemAction
        {
            public string actionTitle => "Cell";
            ItemSlotData _itemSlot;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.bottom;

            public SellAction(ItemSlotData itemSlot)
            {
                _itemSlot = itemSlot;
            }
        }
    }
}