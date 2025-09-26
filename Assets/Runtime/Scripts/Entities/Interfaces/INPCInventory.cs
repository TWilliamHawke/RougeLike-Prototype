using System.Collections.Generic;
using Items;
using Entities.Combat;

namespace Entities.NPC
{
    //TODO split interface
    public interface INPCInventory : IInventory, IEnumerable<ItemContainer>
    {
        WeaponTemplate weapon { get; }
        Dictionary<DamageType, int> resists { get; }
    }
}


