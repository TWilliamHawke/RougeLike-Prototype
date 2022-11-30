using Core;

namespace Items.Actions
{
    public class Drop : IItemAction
    {
        public string actionTitle => "Drop";
        public IItemSlot itemSlot { get; set; }

        public RadialButtonPosition preferedPosition => RadialButtonPosition.bottomRight;

        public Drop(IItemSlot itemSlot)
        {
            this.itemSlot = itemSlot;
        }

        public Drop()
        {
        }

        // public void DoAction()
        // {

        // }

        public bool SlotIsValid(IItemSlot itemSlot)
        {
            return itemSlot.itemSlotContainer == ItemSlotContainers.inventory ||
                itemSlot.itemSlotContainer == ItemSlotContainers.storage;
        }
    }
}