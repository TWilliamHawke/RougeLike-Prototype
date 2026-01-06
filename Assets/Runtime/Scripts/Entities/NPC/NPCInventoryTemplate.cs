using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using System.Linq;

namespace Entities.NPC
{
    [CreateAssetMenu(fileName = "NPCInventory", menuName = "Entities/NPCInventory", order = 0)]
    public class NPCInventoryTemplate : ScriptableObject
    {
        [SerializeField] LootTable _equipmentTable;
        [SerializeField] LootTable _inventory;
        [SerializeField] ItemSectionTemplate _inventorySection;

        public virtual INPCInventory CreateInventory()
        {
            return new NPCInventory(this);
        }

        public ItemSection GetEquipmentItems()
        {
            return _equipmentTable.GetLoot();
        }

        public ItemSection CreateInventorySection()
        {
            ItemSection npcItems = new(_inventorySection);
            npcItems.AddItemsFrom(_inventory);
            return npcItems;
        }

    }
}


