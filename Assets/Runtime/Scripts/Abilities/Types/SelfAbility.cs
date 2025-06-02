using UnityEngine;

namespace Abilities
{
    public class SelfAbility : AbstractAbility
    {
        protected override AbilityTemplate template => _template;

        SelfAbilityTemplate _template { get; init; }

        public SelfAbility(SelfAbilityTemplate template)
        {
            _template = template;
        }
    }
}