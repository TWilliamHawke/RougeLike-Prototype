using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Melee")]
    public class MeleeAbilityTemplate : AbilityTemplate
    {
        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            throw new System.NotImplementedException();
        }

        public override void SelectAbilityController(AbilityController controller)
        {
            throw new System.NotImplementedException();
        }
    }
}