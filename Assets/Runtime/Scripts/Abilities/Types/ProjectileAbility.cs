using System.Text.RegularExpressions;
using Entities.Combat;
using UnityEngine;

namespace Abilities
{
    public class ProjectileAbility : AbstractAbility
    {
        protected override IIconData template => _template;

        ProjectileAbilityTemplate _template;

        [InjectField] ProjectileController _controller;

        public ProjectileAbility(ProjectileAbilityTemplate template)
        {
            _template = template;
        }

        public override void UseOn(IAbilityTarget target)
        {
            if (target is not IRangeAttackTarget validTarget) return;
            _controller.ThrowProjectile(validTarget, _template.projectile);
        }

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            float minDamage = _template.projectile.minDamage * abilityModifiers.magnitudeMult;
            float maxDamage = _template.projectile.maxDamage * abilityModifiers.magnitudeMult;

            var pattern1 = @"%m1";
            var pattern2 = @"%m2";

            var realDescription = Regex.Replace(_template.description, pattern1, minDamage.ToString());
            return Regex.Replace(realDescription, pattern2, maxDamage.ToString());
        }
    }
}