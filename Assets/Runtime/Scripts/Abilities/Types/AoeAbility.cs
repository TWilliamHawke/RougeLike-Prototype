using UnityEngine;

namespace Abilities
{
    public class AoeAbility : AbstractAbility
    {
        protected override IIconData template => _template;

        AoeAbilityTemplate _template;

        [InjectField] AoeAbilityController _controller;

        public AoeAbility(AoeAbilityTemplate template)
        {
            _template = template;
        }

        public override void UseOn(IAbilityTarget target)
        {
            throw new System.NotImplementedException();
        }

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            throw new System.NotImplementedException();
        }
    }
}