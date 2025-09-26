using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "LootTable", menuName = "Items/Loot Table")]
    public class LootTable : ScriptableObject, IDataListSource<IItem>
    {
        [SerializeField] bool _getOnlyOneElenemt;
        [Range(0, 1)]
        [SerializeField] float _chanceOfNone;

        [ContextMenuItem("CheckChildren", "CheckChildren")]
        [SerializeField] LootTable[] _childLootTables;
        [SerializeField] ItemSlotData[] _lootItems;

        IDataListSource<IItem>[] IDataListSource<IItem>.childTables => _childLootTables;
        IDataCount<IItem>[] IDataListSource<IItem>.dataItems => _lootItems;
        bool IDataListSource<IItem>.getOnlyOneElenemt => _getOnlyOneElenemt;
        float IDataListSource<IItem>.chanceOfNone => _chanceOfNone;
        DataListGenerator<IItem> IDataListSource<IItem>.dataListGenerator => _dataListGenerator;

        DataListGenerator<IItem> _dataListGenerator;

        private void OnEnable()
        {
            _dataListGenerator = new DataListGenerator<IItem>(this);
        }

        public void FillItemSection<T>(ref T loot) where T : ILootStorage
        {
            _dataListGenerator.FillDataList(ref loot);
        }

        public void FillItemSection<T>(T loot) where T : ILootStorage
        {
            _dataListGenerator.FillDataList(ref loot);
        }

        public ItemSection GetLoot()
        {
            var section = new ItemSection(new LootSectionTemplate());
            FillItemSection(ref section);
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

            foreach (var itemSlot in loot)
            {
                Debug.Log($"{itemSlot.item.displayName}: {itemSlot.count}");
            }
        }
    }
}