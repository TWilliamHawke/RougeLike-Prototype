namespace Items
{
    public interface IItemSectionInfo
    {
        ItemStorageType itemStorage { get; }
        void Refresh();
    }
}