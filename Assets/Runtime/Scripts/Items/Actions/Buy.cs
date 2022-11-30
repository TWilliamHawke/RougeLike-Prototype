using Core;

namespace Items.Actions
{
    public class Buy : IItemAction
    {
        public string actionTitle => "Buy";

        public IItemSlot itemSlot { get; set; }

        public RadialButtonPosition preferedPosition => RadialButtonPosition.top;

        public Buy(IItemSlot itemSlot)
        {
            this.itemSlot = itemSlot;
        }

        public Buy()
        {
        }

        // public void DoAction()
        // {

        // }

        public bool SlotIsValid(IItemSlot itemSlot)
        {
            return itemSlot.itemSlotContainer == ItemSlotContainers.trader;
        }
    }
}