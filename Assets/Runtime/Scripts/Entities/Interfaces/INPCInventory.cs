using System.Collections.Generic;
using Items;
using Entities.Combat;

namespace Entities.NPC
{
    public interface INPCInventory
    {
        Weapon weapon { get; }
        Dictionary<DamageType, int> resists { get; }
        LootTable loot { get; }
    }
}


