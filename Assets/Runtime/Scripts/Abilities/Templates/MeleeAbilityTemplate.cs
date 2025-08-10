using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Melee")]
    public class MeleeAbilityTemplate : AbilityTemplate
    {
        public override IAbility CreateAbility()
        {
            MeleeAbility ability = new(this);
            abilityController.AddInjectionTarget(ability);
            return ability;
        }
    }
}