using UnityEngine;

namespace Abilities
{
    public class SimpleAbilityContainer : IAbilityContainer
    {
        public bool canBeUsed => true;
        public string displayName => _ability.abilityName;
        public Sprite icon => _ability.abilityIcon;

        IAbility _ability;

        public void UpdateAbilityButton(IAbilityCounterHandler handler)
        {
            handler.HideAbilityCounter();
        }

        public void UseAbility(AbilityController controller)
        {
            _ability.UseBy(controller);
        }
    }

}