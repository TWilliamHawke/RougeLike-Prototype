using UnityEngine;

namespace Abilities
{
    public class MultistageAbility : AbstractAbility
    {
        protected override AbilityTemplate template => _template;

        MultistageAbilityTemplate _template { get; init; }

        public MultistageAbility(MultistageAbilityTemplate template)
        {
            _template = template;
        }
    }
}