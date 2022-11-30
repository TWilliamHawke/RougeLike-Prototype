using Core;

namespace Items.Actions
{
    public class Use : IItemAction
    {
        public string actionTitle => "Use";
        public IItemSlot itemSlot { get; set; }

        public RadialButtonPosition preferedPosition => RadialButtonPosition.top;

        public Use(IItemSlot itemSlot)
        {
            this.itemSlot = itemSlot;
        }

        public Use()
        {
        }

        // public void DoAction()
        // {

        // }

        public bool SlotIsValid(IItemSlot itemSlot)
        {
            return (itemSlot.itemSlotContainer == ItemSlotContainers.inventory ||
                itemSlot.itemSlotContainer == ItemSlotContainers.storage) &&
                itemSlot.itemSlotData.item is IUsable;
        }
    }
}