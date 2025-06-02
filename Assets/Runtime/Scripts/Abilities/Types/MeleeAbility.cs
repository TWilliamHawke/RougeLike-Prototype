using UnityEngine;

namespace Abilities
{
    public class MeleeAbility : AbstractAbility
    {
        protected override AbilityTemplate template => _template;

        MeleeAbilityTemplate _template { get; init; }

        public MeleeAbility(MeleeAbilityTemplate template)
        {
            _template = template;
        }
    }
}