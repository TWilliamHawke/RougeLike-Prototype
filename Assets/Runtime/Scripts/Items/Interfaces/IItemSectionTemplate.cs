namespace Items
{
    public interface IItemSectionTemplate
    {
        string sectionName { get; }
        ItemStorageType storageType { get; }
        int startCapacity { get; }
        bool ItemTypeIsMeet(Item someItem);
    }

}