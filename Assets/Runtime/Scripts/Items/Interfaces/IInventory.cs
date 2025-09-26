namespace Items
{
    public interface IInventory
    {
        void AddItem(IItem item);
        int FindItemCount(IItem item);
        void RemoveOneItem(IItem item);
	}
}