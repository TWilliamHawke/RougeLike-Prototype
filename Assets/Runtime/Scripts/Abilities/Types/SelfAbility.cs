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
        IAbilityUser _user;

        IEffectSource _template;
        [InjectField] SelfAbilityController _controller;

        public SelfAbility(SelfAbilityTemplate template, IAbilityUser user) : this(template, template.effects, user)
        {
        }

        public SelfAbility(IEffectSource template, IEnumerable<SourceEffectData> effects, IAbilityUser user)
        {
            _template = template;
            _effects = effects;
            _user = user;
        }

        public override void Select()
        {
            IAbilityTarget user = _user as IAbilityTarget;
            UseOn(user);
        }

        public override void UseOn(IAbilityTarget target)
        {
            _controller.ApplyEffects(_effects, target, _template);
        }

        public override void UseBy(AbilityController abilityController)
        {
            foreach (var effect in _effects)
            {
                abilityController.ApplyToSelf(effect, _template);
            }
        }
    }
}