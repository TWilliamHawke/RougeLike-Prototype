using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Entities.NPC
{
    [CreateAssetMenu(fileName = "NPCInventory", menuName = "Entities/NPCInventory", order = 0)]
    public class NPCInventoryTemplate : ScriptableObject
    {
		[SerializeField] WeaponTemplate _weapon;

        [SerializeField] LootTable _equipmentTable;
        [SerializeField] LootTable _inventory;
        [SerializeField] ItemSectionTemplate _equipmentSection;

        public WeaponTemplate weapon => _weapon;
        public ItemSectionTemplate equipmentSection => _equipmentSection;

        public LootTable inventoryTable => _inventory;

        public virtual INPCInventory CreateInventory()
        {
            return new NPCInventory(this);
        }

    }
}


