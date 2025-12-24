using UnityEngine;

namespace Abilities
{
    //implemented in Magic/Core
    public abstract class SpellAbilityContainer : AbilityContainer
    {
        public override void UpdateAbilityCounter(IAbilityCounterHandler handler)
        {
            handler.HideAbilityCounter();
        }

    }
}