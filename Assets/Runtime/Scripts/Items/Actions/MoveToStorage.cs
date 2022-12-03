using Core;

namespace Items.Actions
{
    public class MoveToStorage : ItemActionsFactory
    {
        protected override IRadialMenuAction CreateAction(IItemSlot itemSlot)
        {
            return new MoveToStorageAction(itemSlot);
        }

        protected override bool SlotIsValid(IItemSlot itemSlot)
        {
            return itemSlot.itemSlotContainer == ItemSlotContainers.inventory;
        }

        class MoveToStorageAction : IItemAction
        {
            public string actionTitle => "MoveToStorage";
            IItemSlot _itemSlot;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.bottom;

            public MoveToStorageAction(IItemSlot itemSlot)
            {
                _itemSlot = itemSlot;
            }
        }
    }
}