namespace Items.Actions
{
    public class MoveToStorage : IItemAction
    {
        public string actionTitle => "MoveToStorage";

        public void DoAction(ItemSlotData itemSlotData)
        {

        }

        public bool SlotIsValid(IItemSlot itemSlot)
        {
            return itemSlot.itemSlotContainer == ItemSlotContainers.inventory;
        }
    }
}