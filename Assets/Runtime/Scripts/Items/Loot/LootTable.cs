using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "LootTable", menuName = "Items/Loot Table")]
    public class LootTable : DataListGenerator<Item>
    {
		[ContextMenuItem("CheckChildren", "CheckChildren")]
        [SerializeField] LootTable[] _childLootTables;
        [SerializeField] ItemSlotData[] _lootItems;

        protected override DataListGenerator<Item>[] childTables => _childLootTables;

        protected override DataCount<Item>[] dataItems => _lootItems;

        //[SerializeField] LootTableData[] _tables;

        public ItemSection<Item> GetLoot()
        {
            var section = new ItemSection<Item>(-1);
            FillDataList(ref section);
            return section;
        }


        [ContextMenu("Check Generation")]
        protected override void Generate()
        {
			var section = new ItemSection<Item>(-1);
			FillDataList(ref section);

			foreach (var itemSlot in section)
			{
				Debug.Log($"{itemSlot.item.displayName}: {itemSlot.count}");
			}
        }



    }
}