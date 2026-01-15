using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Effect On Self")]
    public class SelfAbilityTemplate : AbilityTemplate, IEffectSource, IAbilityTemplate
    {
        [SerializeField] Injector _effectsHandler;
        [SerializeField] AbilitySoundKit _soundKit;
        [SerializeField] List<SourceEffectData> _effects;

        public AudioClip useSound => _soundKit.useSound;

        public override AbstractAbility CreateAbility()
        {
            SelfAbility ability = new(this);
            ability.BindEffectSource(this);
            _effectsHandler.AddInjectionTarget(ability);
            abilityController.AddInjectionTarget(ability);
            return ability;
        }

        public IEnumerable<ISourceEffectData> GetEffects()
        {
            return _effects;
        }
    }
}