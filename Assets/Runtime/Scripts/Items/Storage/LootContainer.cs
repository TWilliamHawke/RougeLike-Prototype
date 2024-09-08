namespace Items
{
    public class LootContainer : ItemContainer
    {
        public LootContainer()
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


