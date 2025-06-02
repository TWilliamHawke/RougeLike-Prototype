using System.Collections.Generic;
using Items;
using Entities.Combat;

namespace Entities.NPC
{
    //TODO split interface
    public interface INPCInventory : IInventory, IEnumerable<ItemContainer>
    {
        Weapon weapon { get; }
        Dictionary<DamageType, int> resists { get; }
    }
}


