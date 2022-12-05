using Core;

namespace Items.Actions
{
    public class MoveToStorage : ItemActionsFactory
    {
        protected override IRadialMenuAction CreateAction(ItemSlotData itemSlot)
        {
            return new MoveToStorageAction(itemSlot);
        }

        protected override bool SlotIsValid(ItemSlotData itemSlot)
        {
            return itemSlot.slotContainer == ItemContainerType.inventory;
        }

        class MoveToStorageAction : IItemAction
        {
            public string actionTitle => "MoveToStorage";
            ItemSlotData _itemSlot;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.bottom;

            public MoveToStorageAction(ItemSlotData itemSlot)
            {
                _itemSlot = itemSlot;
            }
        }
    }
}