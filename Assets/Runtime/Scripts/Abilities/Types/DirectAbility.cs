using UnityEngine;

namespace Abilities
{
    public class DirectAbility : AbstractAbility
    {
        protected override IIconData template => _template;

        DirectAbilityTemplate _template;

        [InjectField] DirectAbilityController _controller;

        public DirectAbility(DirectAbilityTemplate template)
        {
            _template = template;
        }

        public override void UseOn(IAbilityTarget target)
        {
            _controller.ApplyEffects(_template.effects, target, _template);
        }

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            throw new System.NotImplementedException();
        }
    }
}