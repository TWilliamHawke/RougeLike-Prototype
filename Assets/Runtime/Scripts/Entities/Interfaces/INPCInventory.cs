using System.Collections.Generic;
using Items;
using Entities.Combat;

namespace Entities.NPC
{
    //TODO split interface
    public interface INPCInventory : IEnumerable<ItemContainer>
    {
        Weapon weapon { get; }
        Dictionary<DamageType, int> resists { get; }
        LootTable loot { get; }
    }
}


