using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Direct")]
    public class DirectAbilityTemplate : AbilityTemplate, IAbilityWithTarget, IEffectSource
    {
        [SerializeField] List<SourceEffectData> _effects;

        public IEnumerable<SourceEffectData> effects => _effects;

        public override IAbility CreateAbility()
        {
            DirectAbility ability = new(this);
            abilityController.AddInjectionTarget(ability);
            return ability;
        }

        public bool TargetIsValid(IAbilityTarget target)
        {
            return true;
        }

        public void UseOnTarget(AbilityController _, IAbilityTarget target)
        {
            var effectsStorage = target.GetComponent<EffectsStorage>();
            foreach (var effect in _effects)
            {
                effect.ApplyEffect(effectsStorage, this);
            }
        }
    }
}