using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items.UI
{
	public class InventoryScreen : MonoBehaviour, IUIScreen
	{
		[SerializeField] Inventory _inventory;
	    [SerializeField] InventorySection _potionsBag;
		[SerializeField] InventorySection _scrollsBag;
		[SerializeField] InventorySection _main;
		[SerializeField] StorageButton _storageButton;

		public void Init()
		{
			_potionsBag.SetSectionData(_inventory.potionsBag);
			_scrollsBag.SetSectionData(_inventory.scrollsBag);
			_main.SetSectionData(_inventory.main);
			_storageButton.Init();
		}
	}
}