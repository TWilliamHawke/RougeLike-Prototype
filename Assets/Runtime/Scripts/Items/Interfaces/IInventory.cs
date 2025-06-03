namespace Items
{
    public interface IInventory
    {
        void AddItem(Item item);
        int FindItemCount(Item item);
        void RemoveOneItem(Item item);
	}
}