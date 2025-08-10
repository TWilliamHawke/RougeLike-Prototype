using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using Effects;
using UnityEngine;

namespace Abilities
{
    public class SelfAbility : AbstractAbility
    {
        protected override IIconData template => _template;
        IEnumerable<SourceEffectData> _effects;

        IEffectSource _template;
        [InjectField] SelfAbilityController _controller;

        public SelfAbility(SelfAbilityTemplate template) : this(template, template.effects)
        {
        }

        public SelfAbility(IEffectSource template, IEnumerable<SourceEffectData> effects)
        {
            _template = template;
            _effects = effects;
        }

        public override void Select(IAbilityUser user, IAbilityContainer container)
        {
            IAbilityTarget target = user.GetComponent<IAbilityTarget>();
            if (target is null) return;
            container.UseAbility(target);
        }

        public override void UseOn(IAbilityTarget target)
        {
            _controller.ApplyEffects(_effects, target, _template);
        }

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            var sb = new StringBuilder();
            string pattern1 = @"%m";

            foreach (var effectData in _effects)
            {
                var magnitude = effectData.power * abilityModifiers.magnitudeMult;
                var realDescription = Regex.Replace(effectData.effect.description, pattern1, magnitude.ToString());
                sb.AppendLine(realDescription);
            }

            return sb.ToString();
        }

    }
}