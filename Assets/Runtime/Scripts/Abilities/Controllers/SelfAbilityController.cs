using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Abilities
{
    public class SelfAbilityController : MonoBehaviour
    {
        public void ApplyEffects(IEnumerable<SourceEffectData> effects, IAbilityTarget target, IEffectSource effectSource)
        {
            var effectsStorage = target.GetComponent<EffectsStorage>();
            foreach (var effect in effects)
            {
                effect.ApplyEffect(effectsStorage, effectSource);
            }

        }
    }
}