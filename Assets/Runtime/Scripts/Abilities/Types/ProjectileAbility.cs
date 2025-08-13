using System.Linq;
using System.Text.RegularExpressions;
using Entities.Combat;
using Map;

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

        public override void Use(IAbilityUser user, IAbilityTarget target)
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

        public override bool TileHasValidTarget(IAbilityUser user, ITileClickData tile)
        {
            //TODO add visibility check
            return tile.entitiesOnTile.Any(entity => entity is IAbilityTarget);
        }

        public override IAbilityTarget SelectTarget(ITileClickData tile)
        {
            var target = tile.entitiesOnTile.First(entity => entity is IAbilityTarget);
            return target as IAbilityTarget;
        }
    }
}