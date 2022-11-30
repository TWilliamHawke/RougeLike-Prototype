using Core;

namespace Items.Actions
{
    public class Cell : IItemAction
    {
        public string actionTitle => "Cell";
        public IItemSlot itemSlot { get; set; }

        public RadialButtonPosition preferedPosition => RadialButtonPosition.bottom;

        public Cell(IItemSlot itemSlot)
        {
            this.itemSlot = itemSlot;
        }

        public Cell()
        {
        }

        // public void DoAction()
        // {

        // }

        public void SetData(ItemSlotData data)
        {

        }

        public bool SlotIsValid(IItemSlot itemSlot)
        {
            return itemSlot.itemSlotContainer != ItemSlotContainers.trader && false;
        }
    }
}