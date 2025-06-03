using UnityEngine;

namespace Abilities
{
    public class DirectAbility : AbstractAbility
    {
        protected override IIconData template => _template;

        DirectAbilityTemplate _template { get; init; }

        public DirectAbility(DirectAbilityTemplate template)
        {
            _template = template;
        }

        public override void Use(AbilityController abilityController)
        {
            throw new System.NotImplementedException();
        }
    }
}