using UnityEngine;

namespace Abilities
{
    public class ProjectileAbility : AbstractAbility
    {
        protected override IIconData template => _template;

        ProjectileAbilityTemplate _template { get; init; }

        public ProjectileAbility(ProjectileAbilityTemplate template)
        {
            _template = template;
        }

        public override void Use(AbilityController abilityController)
        {
            throw new System.NotImplementedException();
        }
    }
}