namespace Items
{
    public interface IItemSectionInfo
    {
        ItemContainerType itemContainer { get; }
        void Refresh();
    }
}