using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Items
{
    public class ItemStorage : MonoBehaviour
    {
        public int lockLevel { get; private set; } = 0;
        public bool hasTrap { get; private set; } = false;
        public string storageName { get; init; }

        ItemSection<Item> _itemsInContainer;


        public ItemStorage(ItemStorageData template)
        {
            _itemsInContainer = new ItemSection<Item>(ItemContainerType.trader);
            template.loot.FillItemSection(ref _itemsInContainer);
            storageName = template.storageName;
        }

        public ItemStorage(ItemSection<Item> itemSection, string containerName)
        {
            this.storageName = containerName;
            _itemsInContainer = itemSection;
        }

        public void Unlock()
        {
            lockLevel = 0;
        }

		public void DisarmTrap()
		{
			hasTrap = false;
		}
    }
}


