using System.Collections;
using System.Collections.Generic;

namespace Abilities
{
    public interface IAbilityContainer : IAbilityContainerData
    {
        void UseAbility(IAbilityTarget target);
        void SelectBy(IAbilityUser user);
        bool canBeUsed { get; }
    }
}