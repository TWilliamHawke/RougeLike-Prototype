using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Entities.NPC
{
    [CreateAssetMenu(fileName = "NPCInventory", menuName = "Entities/NPCInventory", order = 0)]
    public partial class NPCInventoryTemplate : ScriptableObject
    {
		[SerializeField] Weapon _weapon;

        [SerializeField] LootTable _inventory;
        [SerializeField] ItemContainerData[] _tradeGoods;

        public Weapon weapon => _weapon;

        public LootTable inventory => _inventory;

        public virtual INPCInventory CreateInventory()
        {
            return new NPCInventory(this);
        }

    }
}


