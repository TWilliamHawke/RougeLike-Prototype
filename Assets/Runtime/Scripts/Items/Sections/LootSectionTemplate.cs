namespace Items
{
    public struct LootSectionTemplate : IItemSectionTemplate
    {
        public int startCapacity => -1;
        public ItemStorageType storageType => ItemStorageType.loot;
        public string sectionName { get; init; }

        public LootSectionTemplate(string sectionName = "Loot")
        {
            this.sectionName = sectionName;
        }

        public bool ItemTypeIsMeet(Item someItem)
        {
            return true;
        }
    }

}