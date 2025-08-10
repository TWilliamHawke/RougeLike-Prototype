using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Entities;
using Entities.Combat;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Projectile")]
    public class ProjectileAbilityTemplate : AbilityTemplate, IAbilityWithTarget
    {
        [SerializeField] ProjectileTemplate _projectile;
        [SerializeField] int _minDamage;
        [SerializeField] int _maxDamage;
        [SerializeField] DamageType _damageType;
        [TextArea(5, 10)]
        [SerializeField] string _description;

        public ProjectileTemplate projectile => _projectile;
        public string description => _description;

        public override IAbility CreateAbility()
        {
            ProjectileAbility ability = new(this);
            abilityController.AddInjectionTarget(ability);
            return ability;
        }

        public bool TargetIsValid(IAbilityTarget target)
        {
            return target.GetComponent<Health>() != null;
        }

        public void UseOnTarget(AbilityController controller, IAbilityTarget target)
        {
            if (target is IRangeAttackTarget)
            {
                controller.GetComponent<ProjectileController>()?.ThrowProjectile(target as IRangeAttackTarget, _projectile);
            }
        }
    }
}