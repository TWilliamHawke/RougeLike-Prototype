using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Entities.NPC
{
    public partial class TraderInventoryTemplate : NPCInventoryTemplate
    {

        protected class TraderInventory : NPCInventory
        {
            List<ItemStorage> _tradeItemStorages = new();

            public TraderInventory(TraderInventoryTemplate template) : base(template)
            {
                foreach(var data in template._tradeItems)
                {
                    var storage = new ItemStorage(data);
                    _tradeItemStorages.Add(storage);
                }
            }

            public override IEnumerator<ItemStorage> GetEnumerator()
            {
                yield return equipment;

                for (int i = 0; i < _tradeItemStorages.Count; i++)
                {
                    yield return _tradeItemStorages[i];
                }
            }

        }
    }
}
