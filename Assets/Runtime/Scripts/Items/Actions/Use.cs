using Core;
using Core.UI;

namespace Items.Actions
{
    public class Use : IItemAction, IHaveModalWindowData
    {
        public string actionTitle => "Use";
        public IItemSlot itemSlot { get; set; }
        IUsable _item;

        public RadialButtonPosition preferedPosition => RadialButtonPosition.top;

        public Use(IItemSlot itemSlot)
        {
            this.itemSlot = itemSlot;
            _item = itemSlot?.itemSlotData?.item as IUsable;
        }

        public Use()
        {
        }

        public void DoAction()
        {

        }

        public bool SlotIsValid(IItemSlot itemSlot)
        {
            return (itemSlot.itemSlotContainer == ItemSlotContainers.inventory ||
                itemSlot.itemSlotContainer == ItemSlotContainers.storage) &&
                _item is not null;
        }

        public bool TryFillModalWindowData(ref ModalWindowData data)
        {
            if(!_item.triggerModalWindow) return false;

            return false;
        }
    }
}