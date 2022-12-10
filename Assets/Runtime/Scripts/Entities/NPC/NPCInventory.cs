using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Entities
{
    [CreateAssetMenu(fileName = "NPCInventory", menuName = "Entities/NPCInventory", order = 0)]
    public class NPCInventory : ScriptableObject
    {
        [SerializeField] NPCInventoryType _inventoryType;
		[SerializeField] Weapon _weapon;

        [SerializeField] LootTable _freeAccessItems;
        [SerializeField] TraderContainer[] _tradeGoods;

        public Weapon weapon => _weapon;
        public NPCInventoryType inventoryType => _inventoryType;

    }

    public enum NPCInventoryType
    {
        normal = 0,
        trader = 1,
        shop = 2,
    }
}


