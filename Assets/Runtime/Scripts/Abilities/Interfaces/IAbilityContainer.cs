using System.Collections;
using System.Collections.Generic;

namespace Abilities
{
    public interface IAbilityContainer : IAbilityContainerData
    {
        void UseAbility(AbilityController controller);
		bool canBeUsed { get; }
    }
}