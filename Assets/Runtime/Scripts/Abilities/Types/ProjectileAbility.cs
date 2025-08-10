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

        public override void UseBy(AbilityController abilityController)
        {
            throw new System.NotImplementedException();
        }

        public override void Select(IAbilityTrigger trigger)
        {
            trigger.TriggerSelectionEvent();
        }

        public override void UseOn(IAbilityTarget target)
        {
            if (target is not IRangeAttackTarget validTarget) return;
            _controller.ThrowProjectile(validTarget, _template.projectile);
        }
    }
}