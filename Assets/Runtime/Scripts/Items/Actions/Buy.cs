namespace Items.Actions
{
    public class Buy : IItemAction
    {
        public string actionTitle => "Buy";

        public void DoAction(ItemSlotData itemSlotData)
        {

        }

        public bool SlotIsValid(IItemSlot itemSlot)
        {
            return itemSlot.itemSlotContainer == ItemSlotContainers.trader;
        }
    }
}