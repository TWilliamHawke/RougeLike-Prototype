using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public interface IAbilityInstruction
    {
        void UseAbility(AbilityController controller);
        Sprite abilityIcon { get; }
		bool canBeUsed { get; }
    }
}