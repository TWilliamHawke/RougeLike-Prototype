using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Entities.NPCScripts
{
    public class TraderContainer : MonoBehaviour
    {
        public int lockLevel { get; private set; } = 0;
        public bool hasTrap { get; private set; } = false;
        public string containerName { get; init; }

        ItemSection<Item> _itemsInContainer;


        public TraderContainer(TraderContainerTemplate template)
        {
            _itemsInContainer = new ItemSection<Item>(ItemContainerType.trader);
            template.loot.FillItemSection(ref _itemsInContainer);
            containerName = template.containerName;
        }

        public TraderContainer(ItemSection<Item> itemSection, string containerName)
        {
            this.containerName = containerName;
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


