using UnityEngine;

namespace Abilities
{
    public class DirectAbility : AbstractAbility
    {
        protected override AbilityTemplate template => _template;

        DirectAbilityTemplate _template { get; init; }

        public DirectAbility(DirectAbilityTemplate template)
        {
            _template = template;
        }
    }
}