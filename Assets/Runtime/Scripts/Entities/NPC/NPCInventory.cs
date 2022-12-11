using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using Entities.Combat;

namespace Entities.NPCScripts
{
    public class NPCInventory
    {
        ItemSection<Item> _freeAccessItems = new(ItemContainerType.trader);

        public Dictionary<DamageType, int> resists { get; init; } = new();

        public Weapon weapon { get; init; }

        public NPCInventory(NPCInventoryTemplate template)
        {
            weapon = template.weapon;
        }
    }
}


