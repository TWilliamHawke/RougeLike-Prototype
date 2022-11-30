using Core;

namespace Items.Actions
{
    public class MoveToStorage : IItemAction
    {
        public string actionTitle => "MoveToStorage";
        public IItemSlot itemSlot { get; set; }

        public RadialButtonPosition preferedPosition => RadialButtonPosition.bottom;

        public MoveToStorage(IItemSlot itemSlot)
        {
            this.itemSlot = itemSlot;
        }

        public MoveToStorage()
        {
        }

        // public void DoAction()
        // {

        // }

        public bool SlotIsValid(IItemSlot itemSlot)
        {
            return itemSlot.itemSlotContainer == ItemSlotContainers.inventory;
        }
    }
}