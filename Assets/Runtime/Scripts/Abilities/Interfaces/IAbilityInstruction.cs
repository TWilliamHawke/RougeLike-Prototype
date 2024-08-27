using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public interface IAbilityInstruction
    {
        void UseAbility(AbilityController controller);
        Sprite abilityIcon { get; }
		bool canBeUsed { get; }
    }
}