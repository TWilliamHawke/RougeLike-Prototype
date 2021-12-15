using System.Collections;
using System.Collections.Generic;
using Entities;
using Entities.Combat;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName ="Ability", menuName ="Abilities/Projectile")]
    public class ProjectileAbility : Ability, IAbilityWithTarget
    {
        [SerializeField] ProjectileTemplate _projectile;
        [SerializeField] int _minDamage;
        [SerializeField] int _maxDamage;
        [SerializeField] DamageType _damageType;

        public bool TargetIsValid(IEffectTarget target)
        {
            return target.GetComponent<Health>() != null;
        }

        public override void Use(AbilityController controller)
        {
            controller.StartTargetSelection(this);
        }

        public void UseOnTarget(AbilityController controller, IEffectTarget target)
        {
            if (target is IRangeAttackTarget)
            {
                controller.GetComponent<ProjectileController>()?.ThrowProjectile(target as IRangeAttackTarget);
            }
        }
    }
}