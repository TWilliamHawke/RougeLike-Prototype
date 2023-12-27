namespace Items
{
    public interface IItemSectionInfo
    {
        ItemStorageType itemContainer { get; }
        void Refresh();
    }
}