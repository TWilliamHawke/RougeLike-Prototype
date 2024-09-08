using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Entities.NPC
{
    [CreateAssetMenu(fileName = "NPCInventory", menuName = "Entities/TraderInventory", order = -1)]
    public partial class TraderInventoryTemplate : NPCInventoryTemplate
    {
        [SerializeField] List<ItemContainerData> _tradeItems;

        public override INPCInventory CreateInventory()
        {
            return new TraderInventory(this);
        }
    }
}
