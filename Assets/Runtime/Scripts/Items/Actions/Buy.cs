using Core;

namespace Items.Actions
{
    public class Buy : ItemActionsFactory
    {
        protected override IRadialMenuAction CreateAction(ItemSlotData itemSlotData)
        {
            return new BuyAction(itemSlotData);
        }

        protected override bool SlotIsValid(ItemSlotData itemSlotitemSlotData)
        {
            return itemSlotitemSlotData.slotContainer == ItemStorageType.trader;
        }

        class BuyAction : IItemAction
        {
            public string actionTitle => "Buy";
            public RadialButtonPosition preferedPosition => RadialButtonPosition.top;
            
            ItemSlotData _itemSlotData;

            public BuyAction(ItemSlotData itemSlotData)
            {
                _itemSlotData = itemSlotData;
            }
        }
    }
}