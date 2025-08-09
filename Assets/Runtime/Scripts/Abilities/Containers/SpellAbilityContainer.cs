using UnityEngine;

namespace Abilities
{
    //implemented in Magic/Core
    public abstract class SpellAbilityContainer : IAbilityContainer
    {
        public abstract bool canBeUsed { get; }
        public abstract string displayName { get; }
        public abstract Sprite icon { get; }

        public abstract void UseAbility(AbilityController controller);

        public void UpdateAbilityButton(IAbilityCounterHandler handler)
        {
            handler.HideAbilityCounter();
        }

    }
}