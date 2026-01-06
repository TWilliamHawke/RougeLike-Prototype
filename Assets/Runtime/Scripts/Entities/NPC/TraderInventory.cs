using System.Collections;
using System.Collections.Generic;
using Items;
using UnityEngine;

namespace Entities.NPC
{
    public class TraderInventory : NPCInventory
    {
        List<ItemContainer> _tradeItems = new();

        public TraderInventory(TraderInventoryTemplate template) : base(template)
        {
            foreach(var data in template.tradeItems)
            {
                var container = new ItemContainer(data);
                _tradeItems.Add(container);
            }
        }

        public override IEnumerator<ItemContainer> GetEnumerator()
        {
            yield return _npcItems;

            for (int i = 0; i < _tradeItems.Count; i++)
            {
                yield return _tradeItems[i];
            }
        }

        ItemContainer FindContainer(int idx)
        {
            if (idx == 0)
            {
                return _npcItems;
            }
            else
            {
                return _tradeItems[idx - 1];
            }
        }

    }
}
