using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Abilities
{
    public class SelfAbility : AbstractAbility
    {
        protected override IIconData template => _template;
        IEnumerable<SourceEffectData> _effects;

        IEffectSource _template { get; init; }

        public SelfAbility(SelfAbilityTemplate template)
        {
            _template = template;
            _effects = template.effects;
        }

        public SelfAbility(IEffectSource template, IEnumerable<SourceEffectData> effects)
        {
            _template = template;
            _effects = effects;
        }

        public override void Use(AbilityController abilityController)
        {
            foreach (var effect in _effects)
            {
                abilityController.ApplyToSelf(effect, _template);
            }
        }
    }
}