using UnityEngine;

namespace Abilities
{
    public class MultistageAbility : AbstractAbility
    {
        protected override IIconData template => _template;

        MultistageAbilityTemplate _template { get; init; }

        public MultistageAbility(MultistageAbilityTemplate template)
        {
            _template = template;
        }

        public override void Use(AbilityController abilityController)
        {
            throw new System.NotImplementedException();
        }
    }
}