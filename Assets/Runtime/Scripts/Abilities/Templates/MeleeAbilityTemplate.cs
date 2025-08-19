using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Melee")]
    public class MeleeAbilityTemplate : AbilityTemplate, IEffectSource
    {
        [SerializeField] Injector _effectsHandler;

        public override IAbility CreateAbility()
        {
            MeleeAbility ability = new(this);
            abilityController.AddInjectionTarget(ability);
            _effectsHandler.AddInjectionTarget(ability);
            return ability;
        }

        public IEnumerable<ISourceEffectData> GetEffects()
        {
            yield break;
        }
    }
}