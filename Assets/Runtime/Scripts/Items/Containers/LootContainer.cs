using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class LootContainer : ItemContainer, IContainersList
    {
        public override ItemStorageType storageType => ItemStorageType.loot;
        public int count => 1;

        public LootContainer() : base(new ItemSection(new LootSectionTemplate("Loot")))
        {
        }

        public void AddItemsFrom(LootTable lootTable)
        {
            lootTable.FillItemSection(_itemsSection);
        }

        public void AddItemsFrom(IEnumerable<ItemSlotData> itemSlots)
        {
            itemSlots.ForEach(itemSlot => _itemsSection.AddItems(itemSlot));
        }

        public void RemoveItems(IEnumerable<ItemSlotData> itemSlots)
        {
            //UNDONE add itemSection.removeitems() logic
        }

        public ItemContainer ContainerAt(int idx)
        {
            return this;
        }

        public IEnumerable<ItemContainer> GetAllContainers()
        {
            yield return this;
        }

        public bool IsEmpty()
        {
            return isEmpty;
        }
    }
}


