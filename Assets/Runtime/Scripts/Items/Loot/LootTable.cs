using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "LootTable", menuName = "Items/Loot Table")]
    public class LootTable : DataListTable<ItemTemplate>
    {
        [ContextMenuItem("CheckChildren", "CheckChildren")]
        [SerializeField] List<LootTableData> _lootTables = new();
        [SerializeField] List<LootItemsData> _lootItems = new();

        protected override IEnumerable<IDataListElementSource<ItemTemplate>> childTables => _lootTables;
        protected override IEnumerable<IDataListElementSource<ItemTemplate>> childElements => _lootItems;

        public void FillItemSection<T>(T lootSection) where T : ILootSection
        {
            int rarity = 10;
            LoootItemsList itemsList = new(rarity);
            var rawLoot = GetElements();
            itemsList.AddRawLoot(rawLoot);
            itemsList.TransferItemsToSection(lootSection);
            itemsList.Clear();
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
            HashSet<LootTable> tables = new();
            CheckErrors(tables);
        }

        private void CheckErrors(HashSet<LootTable> existingTables)
        {
            if (existingTables.Contains(this))
            {
                Debug.Log("Loop detected in " + name);
                throw new DataListGeneratorException<LootTable>(this);
            }
            existingTables.Add(this);

            _lootTables ??= new();
            foreach (var tableData in _lootTables)
            {
                HashSet<LootTable> clone = new(existingTables);
                tableData.CheckErrors(clone);
            }
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

        #region Supporting classes
        [System.Serializable]
        public class LootTableData : IDataListElementSource<ItemTemplate>
        {
            [SerializeField] LootTable _table;
            [PlusMinusBtn]
            [SerializeField] IntValue _count = 1;
            [PlusMinusBtn]
            [SerializeField] int _weight = 1;

            public int weight => _weight;
            public LootTable table => _table;

            public void CheckErrors(HashSet<LootTable> existingTables)
            {
                if (!_table) return;
                _table.CheckErrors(existingTables);
            }

            public IEnumerable<IDataListElement<ItemTemplate>> GetElements()
            {
                for (int i = 0; i < _count.minValue; i++)
                {
                    foreach (var element in _table.GetElements())
                    {
                        yield return element;
                    }
                }
            }
        }

        [System.Serializable]
        public class LootItemsData : IDataListElementSource<ItemTemplate>
        {
            [SerializeField] ItemTemplate _item;
            [SerializeField] IntValue _count = 1;
            [PlusMinusBtn]
            [SerializeField] int _weight = 1;

            public int weight => _weight;

            public IEnumerable<IDataListElement<ItemTemplate>> GetElements()
            {
                yield return new DataListElement<ItemTemplate>
                {
                    element = _item,
                    count = _count
                };
            }
        }
        #endregion
    }
}