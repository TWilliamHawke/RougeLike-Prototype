using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "LootTable", menuName = "Items/Loot Table")]
    public class LootTable : ScriptableObject, IDataListSource<Item>
    {
        [SerializeField] bool _getOnlyOneElenemt;
        [Range(0, 1)]
        [SerializeField] float _chanceOfNone;

        [ContextMenuItem("CheckChildren", "CheckChildren")]
        [SerializeField] LootTable[] _childLootTables;
        [SerializeField] ItemSlotData[] _lootItems;

        IDataListSource<Item>[] IDataListSource<Item>.childTables => _childLootTables;
        IDataCount<Item>[] IDataListSource<Item>.dataItems => _lootItems;
        bool IDataListSource<Item>.getOnlyOneElenemt => _getOnlyOneElenemt;
        float IDataListSource<Item>.chanceOfNone => _chanceOfNone;
        DataListGenerator<Item> IDataListSource<Item>.dataListGenerator => _dataListGenerator;

        DataListGenerator<Item> _dataListGenerator;

        private void OnEnable()
        {
            _dataListGenerator = new DataListGenerator<Item>(this);
        }

        public void FillItemSection(ref ItemSection<Item> loot)
        {
            _dataListGenerator.FillDataList(ref loot);
        }

        public ItemSection<Item> GetLoot()
        {
            var section = new ItemSection<Item>(ItemContainerType.loot);
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