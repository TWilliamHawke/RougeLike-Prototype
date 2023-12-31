using System.Collections.Generic;
using Items;
using Entities.Combat;

namespace Entities.NPC
{
    //TODO split interface
    public interface INPCInventory : IEnumerable<ItemStorage>
    {
        Weapon weapon { get; }
        Dictionary<DamageType, int> resists { get; }
        LootTable loot { get; }
        ItemStorage this[int idx] { get; }
        int storageCount { get; }
        void DeselectItem(ItemSlotData item);
        IEnumerable<ItemSlotData> GetSelectedItems();
    }
}


