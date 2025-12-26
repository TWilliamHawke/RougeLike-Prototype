using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	[CreateAssetMenu(fileName = "InventoryIterator", menuName = "Items/InventoryIterator")]
    public class InventoryIterator : ScriptableObject
	{
		[SerializeField] Inventory _inventory;
		[SerializeField] ItemSectionTemplate _mainSectionTemplate;
		[SerializeField] ItemSectionTemplate _storageTemplate;

		public IEnumerable<ItemSlotData> GetMainItems()
		{
			return _inventory.GetSection(_mainSectionTemplate);
		}

		public IEnumerable<ItemSlotData> GetStorageItems()
		{
			return _inventory.GetSection(_storageTemplate);
		}
	}
}