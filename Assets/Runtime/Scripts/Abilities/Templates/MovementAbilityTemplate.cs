using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Movement")]
    public class MovementAbilityTemplate : AbilityTemplate
    {
        public override IAbility CreateAbility()
        {
            MovementAbility ability = new(this);
            abilityController.AddInjectionTarget(ability);
            return ability;
        }
    }
}