using Core;

namespace Items.Actions
{
    public class Drop : ItemActionsFactory
    {
        protected override IRadialMenuAction CreateAction(ItemSlotData itemSlot)
        {
            return new DropAction(itemSlot);
        }

        protected override bool SlotIsValid(ItemSlotData itemSlot)
        {
            return itemSlot.slotContainer == ItemStorageType.inventory ||
                itemSlot.slotContainer == ItemStorageType.storage;
        }

        class DropAction : IRadialMenuAction
        {
            public string actionTitle => "Drop";
            ItemSlotData _itemSlot;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.bottomRight;

            public DropAction(ItemSlotData itemSlot)
            {
                _itemSlot = itemSlot;
            }

            public void DoAction()
            {
                _itemSlot.RemoveAllItems();
            }
        }
    }
}