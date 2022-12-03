using Core;
using Core.UI;

namespace Items.Actions
{
    public class Use : ItemActionsFactory
    {
        protected override IRadialMenuAction CreateAction(IItemSlot itemSlot)
        {
            return new UseAction(itemSlot);
        }

        protected override bool SlotIsValid(IItemSlot itemSlot)
        {
            return (itemSlot.itemSlotContainer == ItemSlotContainers.inventory ||
                itemSlot.itemSlotContainer == ItemSlotContainers.storage) &&
                itemSlot?.itemSlotData?.item is IUsable;
        }

        class UseAction : IItemAction, IHaveModalWindowData
        {
            public string actionTitle => "Use";
            IItemSlot _itemSlot;
            IUsable _item;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.top;

            public UseAction(IItemSlot itemSlot)
            {
                _itemSlot = itemSlot;
                _item = itemSlot?.itemSlotData?.item as IUsable;
            }

            public bool TryFillModalWindowData(ref ModalWindowData data)
            {
                if (!_item.triggerModalWindow) return false;

                return false;
            }

        }
    }
}