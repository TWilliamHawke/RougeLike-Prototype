using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.NPC
{
    public partial class TraderInventoryTemplate : NPCInventoryTemplate
    {

        protected class TraderInventory : NPCInventory
        {
            public TraderInventory(TraderInventoryTemplate template) : base(template)
            {
            }
        }
    }
}
