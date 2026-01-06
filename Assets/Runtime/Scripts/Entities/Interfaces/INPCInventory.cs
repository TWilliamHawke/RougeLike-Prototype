using System.Collections.Generic;
using Items;
using Abilities;

namespace Entities.NPC
{
    public interface INPCInventory : IInventory, IEnumerable<ItemContainer>
    {
        IEnumerable<IAbilityContainer> GetItemAbilities(IAbilitiesFactory factory);
    }
}
