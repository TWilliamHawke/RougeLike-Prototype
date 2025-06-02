using UnityEngine;

namespace Abilities
{
    public class MovementAbility : AbstractAbility
    {
        protected override AbilityTemplate template => _template;

        MovementAbilityTemplate _template { get; init; }

        public MovementAbility(MovementAbilityTemplate template)
        {
            _template = template;
        }
    }
}