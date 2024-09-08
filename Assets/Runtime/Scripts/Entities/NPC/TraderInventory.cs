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
            List<ItemContainer> _tradeItems = new();
            public override ItemContainer this[int idx] => FindContainer(idx);
            public override int sectionsCount => _tradeItems.Count + 1;

            public TraderInventory(TraderInventoryTemplate template) : base(template)
            {
                foreach(var data in template._tradeItems)
                {
                    var container = new ItemContainer(data);
                    _tradeItems.Add(container);
                }
            }

            public override IEnumerator<ItemContainer> GetEnumerator()
            {
                yield return equipment;

                for (int i = 0; i < _tradeItems.Count; i++)
                {
                    yield return _tradeItems[i];
                }
            }

            ItemContainer FindContainer(int idx)
            {
                if (idx == 0)
                {
                    return equipment;
                }
                else
                {
                    return _tradeItems[idx - 1];
                }
            }

        }
    }
}
