using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Entities.NPCScripts
{
    [CreateAssetMenu(fileName = "NPCInventory", menuName = "Entities/NPCInventory", order = 0)]
    public class NPCInventoryTemplate : ScriptableObject
    {
        [SerializeField] NPCInventoryType _inventoryType;
		[SerializeField] Weapon _weapon;

        [SerializeField] LootTable _freeAccessItems;
        [SerializeField] TraderContainerTemplate[] _tradeGoods;

        public Weapon weapon => _weapon;
        public NPCInventoryType inventoryType => _inventoryType;

        public LootTable freeAccessItems => _freeAccessItems;

    }

    public enum NPCInventoryType
    {
        normal = 0,

        //_freeAccessItems can be used in battle, not for purchase
        trader = 1,

        //cannot be attacked
        //_freeAccessItems can be purchased
        shop = 2,
    }
}


