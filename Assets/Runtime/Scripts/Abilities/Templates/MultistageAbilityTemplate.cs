using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Effect on target")]
    public class MultistageAbilityTemplate : AbilityTemplate
    {
        public override AbstractAbility CreateAbility()
        {
            MultistageAbility ability = new(this);
            abilityController.AddInjectionTarget(ability);
            return ability;
        }
    }
}