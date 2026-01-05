namespace Items
{
    public struct LootSectionTemplate : IItemSectionTemplate
    {
        public int startCapacity => -1;
        public ItemStorageType storageType => ItemStorageType.loot;
        public string sectionName { get; init; }

        public LootSectionTemplate(string sectionName)
        {
            this.sectionName = sectionName;
        }

        public bool ItemTypeIsMeet(IItem someItem)
        {
            return true;
        }
    }

}