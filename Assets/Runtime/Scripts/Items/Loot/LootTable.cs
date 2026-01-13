using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "LootTable", menuName = "Items/Loot Table")]
    public class LootTable : ScriptableObject, IDataListTable<ItemTemplate>
    {
        [SerializeField] bool _getOnlyOneElenemt;
        [Range(0, 1)]
        [SerializeField] float _chanceOfNone;

        [ContextMenuItem("CheckChildren", "CheckChildren")]
        [SerializeField] LootTable[] _childLootTables;
        [SerializeField] LootItemsData[] _lootItems;

        IDataListTable<ItemTemplate>[] IDataListTable<ItemTemplate>.childTables => _childLootTables;
        IDataListElement<ItemTemplate>[] IDataListTable<ItemTemplate>.dataItems => _lootItems;
        bool IDataListTable<ItemTemplate>.getOnlyOneElenemt => _getOnlyOneElenemt;
        float IDataListTable<ItemTemplate>.chanceOfNone => _chanceOfNone;
        DataListGenerator<ItemTemplate> IDataListTable<ItemTemplate>.dataListGenerator => _dataListGenerator;

        DataListGenerator<ItemTemplate> _dataListGenerator;

        private void OnEnable()
        {
            _dataListGenerator = new DataListGenerator<ItemTemplate>(this);
        }

        public void FillItemSection<T>(T lootStorage) where T : ILootSection
        {
            LoootItemsList itemsList = new(lootStorage);
            _dataListGenerator.FillDataList(ref itemsList);
            itemsList.CreateItems();
        }

        public ItemSection GetLoot()
        {
            var section = new ItemSection(new LootSectionTemplate());
            FillItemSection(section);
            return section;
        }

        [ContextMenu("CheckChildren")]
        public void CheckErrors()
        {
            _dataListGenerator.CheckErrors();
        }

        [ContextMenu("Check Generation")]
        void Generate()
        {
            var loot = GetLoot();

            foreach (var itemSlot in loot.GetItems())
            {
                Debug.Log($"{itemSlot.item.displayName}: {itemSlot.count}");
            }
        }
    }
}