using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/AOE")]
    public class AoeAbilityTemplate : AbilityTemplate
    {
        public override IAbility CreateAbility()
        {
            AoeAbility ability = new(this);
            abilityController.AddInjectionTarget(ability);
            return ability;
        }
    }
}