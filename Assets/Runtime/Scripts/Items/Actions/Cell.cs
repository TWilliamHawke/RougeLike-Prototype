namespace Items.Actions
{
    public class Cell : IItemAction
    {
        public string actionTitle => "Cell";

        public void DoAction(ItemSlotData itemSlotData)
        {

        }

        public bool SlotIsValid(IItemSlot itemSlot)
        {
            return itemSlot.itemSlotContainer != ItemSlotContainers.trader && false;
        }
    }
}