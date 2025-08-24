using Core;

namespace Items.Actions
{
    public class Drop : ContextActionFactory<ItemSlotData>
    {
        protected override ContextActionContainer CreateAction(ItemSlotData itemSlot)
        {
            return new DropAction(itemSlot);
        }

        protected override bool ElementIsValid(ItemSlotData itemSlot)
        {
            return true;
        }

        class DropAction : ContextActionContainer
        {
            ItemSlotData _itemSlot;

            public DropAction(ItemSlotData itemSlot)
            {
                _itemSlot = itemSlot;
            }

            public override void DoAction()
            {
                _itemSlot.RemoveAllItems();
            }
        }
    }
}