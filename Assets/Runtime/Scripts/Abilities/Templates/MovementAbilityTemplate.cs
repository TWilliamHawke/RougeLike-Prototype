using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Movement")]
    public class MovementAbilityTemplate : AbilityTemplate
    {
        public override IAbility CreateAbility(IAbilityUser user)
        {
            MovementAbility ability = new(this);
            abilityController.AddInjectionTarget(ability);
            return ability;
        }

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