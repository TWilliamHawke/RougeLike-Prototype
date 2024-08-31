namespace Items
{
    public struct DefaultSection : IItemSectionTemplate
    {
        public int startCapacity => -1;
        public ItemStorageType storageType => ItemStorageType.none;
        public string sectionName { get; init; }

        public DefaultSection(string sectionName = "Default")
        {
            this.sectionName = sectionName;
        }

        public bool ItemTypeIsMeet(Item someItem)
        {
            return true;
        }
    }

}