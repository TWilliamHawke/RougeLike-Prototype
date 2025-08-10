using UnityEngine;

namespace Abilities
{
    public class MultistageAbility : AbstractAbility
    {
        protected override IIconData template => _template;

        MultistageAbilityTemplate _template;

        [InjectField] MultistageAbilityController _controller;

        public MultistageAbility(MultistageAbilityTemplate template)
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