using Effects;
using UnityEngine;

namespace Abilities
{
    public class AbilityEfffectsHandler : MonoBehaviour
    {
        public void ApplyEffects(IAbilityUser user, IAbilityTarget target, IEffectSource effectSource)
        {
            var effectsStorage = target.GetEntityComponent<EffectsStorage>();
            var effects = effectSource.GetEffects();
            foreach (var effect in effects)
            {
                effect.ApplyEffect(effectsStorage, effectSource);
            }
        }
    }
}