using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "LootTable", menuName = "Items/Loot Table")]
    public class LootTable : ScriptableObject
    {
        [SerializeField] bool _getOnlyOneElenemt;
        [Range(0, 1)]
        [SerializeField] float _chanceOfNone;

		[ContextMenuItem("CheckChildren", "CheckChildren")]
        [SerializeField] LootTable[] _childLootTables;
        [SerializeField] ItemSlotData[] _lootItems;

        //[SerializeField] LootTableData[] _tables;

        public ItemSection<Item> GetLoot()
        {
            var section = new ItemSection<Item>(-1);
            GetLoot(ref section);
            return section;
        }

        public void GetLoot(ref ItemSection<Item> loot)
        {
            if (Random.Range(0f, 1f) < _chanceOfNone) return;

            if (_getOnlyOneElenemt)
            {
                GetRandomLoot(ref loot);
            }
            else
            {
                GetAllLoot(ref loot);
            }

        }

        [ContextMenu("Check Generation")]
        void Generate()
        {
			var section = new ItemSection<Item>(-1);
			GetLoot(ref section);

			foreach (var itemSlot in section)
			{
				Debug.Log($"{itemSlot.item.displayName}: {itemSlot.count}");
			}
        }

		[ContextMenu("CheckChildren")]
		public void CheckErrors()
		{
			var set = new HashSet<LootTable>();

			CheckTableInSet(ref set);
		}

		void CheckTableInSet(ref HashSet<LootTable> set)
		{
			if(set.Contains(this))
			{
				throw new LootTableException(this);
			}
			else
			{
				set.Add(this);
				foreach (var lootTable in _childLootTables)
				{
					lootTable.CheckTableInSet(ref set);
				}
                set.Remove(this);
			}
		}

        private void GetRandomLoot(ref ItemSection<Item> loot)
        {
            int variantsCount = _childLootTables.Length + _lootItems.Length;
            if (variantsCount == 0) return;

            int index = Random.Range(0, _childLootTables.Length + _lootItems.Length);

            if (_childLootTables.Length > 0 && index < _childLootTables.Length)
            {
                _childLootTables[index].GetLoot(ref loot);
            }
            else
            {
                var itemSlot = _lootItems[index];
                loot.AddItems(itemSlot.item, itemSlot.count);
            }

        }

        private void GetAllLoot(ref ItemSection<Item> loot)
        {
            foreach (var lootTable in _childLootTables)
            {
                lootTable.GetLoot(ref loot);
            }

            foreach (var itemSlot in _lootItems)
            {
                loot.AddItems(itemSlot.item, itemSlot.count);
            }
        }

    }
}