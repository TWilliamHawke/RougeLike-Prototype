using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Summon")]
    public class SummonAbilityTemplate : AbilityTemplate
    {
        public override AbstractAbility CreateAbility()
        {
            SummonAbility ability = new(this);
            abilityController.AddInjectionTarget(ability);
            return ability;
        }
    }
}