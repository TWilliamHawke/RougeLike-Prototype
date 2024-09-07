namespace Items
{
    public class LootStorage : ItemStorage
    {
        public LootStorage()
        {
            _itemsSection = new ItemSection(new LootSectionTemplate());
        }

        public override ItemStorageType storageType => ItemStorageType.loot;

        public void AddItemsFrom(LootTable lootTable)
        {
            lootTable.FillItemSection(ref _itemsSection);
        }
    }
}


