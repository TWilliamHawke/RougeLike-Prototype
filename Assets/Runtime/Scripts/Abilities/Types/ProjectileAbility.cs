using UnityEngine;

namespace Abilities
{
    public class ProjectileAbility : AbstractAbility
    {
        protected override AbilityTemplate template => _template;

        ProjectileAbilityTemplate _template { get; init; }

        public ProjectileAbility(ProjectileAbilityTemplate template)
        {
            _template = template;
        }
    }
}