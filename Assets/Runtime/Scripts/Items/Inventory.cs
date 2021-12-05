using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	public class Inventory : ScriptableObject
	{
		[SerializeField] Item _testItem;
		
	    InventorySection<Potion> _potionsBag;
		InventorySection<Item> _main;
		InventorySection<Item> _storage;

		List<IInventorySection> _sections;


		public void Init()
		{
			_potionsBag = new InventorySection<Potion>(3);
			_main = new InventorySection<Item>(12);
			_storage = new InventorySection<Item>(-1);
			_sections = new List<IInventorySection>(3);

			_sections.Add(_potionsBag);
			//if special sections is full - add to main
			_sections.Add(_main);
			//if main is full - add to storage
			_sections.Add(_storage);
		}

		public void AddItem(Item item)
		{
			foreach (var section in _sections)
			{
				if(section.ItemMeet(item))
				{
					section.AddItem(item);
					break;
				}
			}
		}

		[ContextMenu("Add TestItem")]
		void AddTestItem()
		{
			AddItem(_testItem);
		}

		[ContextMenu("Clear")]
		void Clear()
		{
			foreach (var section in _sections)
			{
				section.Clear();
			}
		}
	}
}