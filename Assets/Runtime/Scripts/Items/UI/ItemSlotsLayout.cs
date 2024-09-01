namespace Items.UI
{
    public class ItemSlotsLayout : UILayoutWithObserver<ItemSlotData, ItemSlot>
    {
        public void ShowLayout()
        {
            SetLayoutVisibility(true);
        }

        public void HideLayout()
        {
            SetLayoutVisibility(false);
        }
    }
}