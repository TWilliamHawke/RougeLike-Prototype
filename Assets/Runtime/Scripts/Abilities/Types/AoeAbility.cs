using UnityEngine;

namespace Abilities
{
    public class AoeAbility : AbstractAbility
    {
        protected override IIconData template => _template;

        AoeAbilityTemplate _template { get; init; }

        public AoeAbility(AoeAbilityTemplate template)
        {
            _template = template;
        }

        public override void Use(AbilityController abilityController)
        {
            throw new System.NotImplementedException();
        }
    }
}