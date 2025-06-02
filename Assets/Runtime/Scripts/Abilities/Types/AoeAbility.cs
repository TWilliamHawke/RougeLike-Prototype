using UnityEngine;

namespace Abilities
{
    public class AoeAbility : AbstractAbility
    {
        protected override AbilityTemplate template => _template;

        AoeAbilityTemplate _template { get; init; }

        public AoeAbility(AoeAbilityTemplate template)
        {
            _template = template;
        }
    }
}