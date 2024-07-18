using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Entities;
using Entities.Combat;
using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Projectile")]
    public class ProjectileAbility : Ability, IAbilityWithTarget
    {
        [SerializeField] ProjectileTemplate _projectile;
        [SerializeField] int _minDamage;
        [SerializeField] int _maxDamage;
        [SerializeField] DamageType _damageType;
        [TextArea(5, 10)]
        [SerializeField] string _description;

        public bool TargetIsValid(IAbilityTarget target)
        {
            return target.GetComponent<Health>() != null;
        }

        public override void SelectControllerUsage(AbilityController controller)
        {
            controller.StartTargetSelection(this);
        }

        public void UseOnTarget(AbilityController controller, IAbilityTarget target)
        {
            if (target is IRangeAttackTarget)
            {
                controller.GetComponent<ProjectileController>()?.ThrowProjectile(target as IRangeAttackTarget, _projectile);
            }
        }

        public override string GetDescription(AbilityModifiers abilityModifiers)
        {
            float minDamage = _minDamage * abilityModifiers.magnitudeMult;
            float maxDamage = _maxDamage * abilityModifiers.magnitudeMult;

            var pattern1 = @"%m1";
            var pattern2 = @"%m2";

            var realDescription = Regex.Replace(_description, pattern1, minDamage.ToString());
            return Regex.Replace(realDescription, pattern2, maxDamage.ToString());
        }
    }
}