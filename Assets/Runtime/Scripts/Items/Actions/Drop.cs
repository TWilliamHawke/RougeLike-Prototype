namespace Items.Actions
{
    public class Drop : IItemAction
    {
        public string actionTitle => "Drop";

        public void DoAction(ItemSlotData itemSlotData)
        {

        }

        public bool SlotIsValid(IItemSlot itemSlot)
        {
            return itemSlot.itemSlotContainer == ItemSlotContainers.inventory ||
                itemSlot.itemSlotContainer == ItemSlotContainers.storage;
        }
    }
}