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

        public override void AddItemsTo(IItemStorage storage)
        {
            base.AddItemsTo(storage);
            _tradeItems.ForEach(container => storage.AddItemsFrom(container));
        }

        public override void RemoveItemsFrom(IItemStorage storage)
        {
            base.RemoveItemsFrom(storage);
            _tradeItems.ForEach(container => storage.RemoveItems(container));
        }
    }
}
