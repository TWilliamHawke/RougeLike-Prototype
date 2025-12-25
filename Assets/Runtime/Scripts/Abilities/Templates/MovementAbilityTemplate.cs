using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Movement")]
    public class MovementAbilityTemplate : AbilityTemplate
    {
        [SerializeField] bool _useOnSelf = true;

        public bool useOnSelf => _useOnSelf;

        public override AbstractAbility CreateAbility()
        {
            MovementAbility ability = new(this);
            abilityController.AddInjectionTarget(ability);
            return ability;
        }
    }
}