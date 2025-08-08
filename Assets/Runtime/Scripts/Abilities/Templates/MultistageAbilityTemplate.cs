using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Effect on target")]
    public class MultistageAbilityTemplate : AbilityTemplate
    {
        public override IAbility CreateAbility(IAbilityUser user)
        {
            MultistageAbility ability = new(this);
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