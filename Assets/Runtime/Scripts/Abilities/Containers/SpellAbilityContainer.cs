using UnityEngine;

namespace Abilities
{
    //implemented in Magic/Core
    public abstract class SpellAbilityContainer : AbilityContainer
    {
        public override void UpdateAbilityButton(IAbilityCounterHandler handler)
        {
            handler.HideAbilityCounter();
        }

    }
}