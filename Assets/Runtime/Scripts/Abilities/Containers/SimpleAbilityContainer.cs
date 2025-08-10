using UnityEngine;

namespace Abilities
{
    public class SimpleAbilityContainer : AbilityContainer
    {
        public override bool canBeUsed => true;

        public SimpleAbilityContainer(IAbility ability)
        {
            _ability = ability;
        }

        public override void UpdateAbilityButton(IAbilityCounterHandler handler)
        {
            handler.HideAbilityCounter();
        }

        public override void UseAbility(AbilityController controller)
        {
            _ability.UseBy(controller);
        }
    }

}