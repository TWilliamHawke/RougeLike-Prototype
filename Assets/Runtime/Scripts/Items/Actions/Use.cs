namespace Items.Actions
{
    public class Use : IItemAction
    {
        public string actionTitle => "Use";

        public void DoAction(ItemSlotData itemSlotData)
        {

        }

        public bool SlotIsValid(IItemSlot itemSlot)
        {
            return (itemSlot.itemSlotContainer == ItemSlotContainers.inventory ||
                itemSlot.itemSlotContainer == ItemSlotContainers.storage) &&
                itemSlot.itemSlotData.item is IUsable;
        }
    }
}