using System.Collections.Generic;
using Items;
using Abilities;

namespace Entities.NPC
{
    public interface INPCInventory : IInventory
    {
        IEnumerable<IAbilityContainer> GetItemAbilities(IAbilitiesFactory factory);
        void AddItemsTo(IItemStorage storage);
        void RemoveItemsFrom(IItemStorage storage);
    }
}
