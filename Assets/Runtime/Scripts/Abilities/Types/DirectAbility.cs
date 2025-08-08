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

        public override void UseBy(AbilityController abilityController)
        {
            throw new System.NotImplementedException();
        }

        public override void Select()
        {
            //start target selection
        }

        public override void UseOn(IAbilityTarget target)
        {
            _controller.ApplyEffects(_template.effects, target, _template);
        }
    }
}