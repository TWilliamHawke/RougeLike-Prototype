using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Effects;
using Entities.Combat;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Projectile")]
    public class ProjectileAbilityTemplate : AbilityTemplate,
        IEffectSource
    {
        [SerializeField] ProjectileTemplate _projectile;
        [SerializeField] int _minDamage;
        [SerializeField] int _maxDamage;
        [SerializeField] DamageType _damageType;
        [TextArea(5, 10)]
        [SerializeField] string _description;

        public ProjectileTemplate projectile => _projectile;

        public override IAbility CreateAbility()
        {
            ProjectileAbility ability = new(this);
            abilityController.AddInjectionTarget(ability);
            return ability;
        }

        public IEnumerable<SourceEffectData> GetEffects()
        {
            return default;
        }

        public  string GetDescription(AbilityModifiers abilityModifiers)
        {
            float minDamage = _projectile.minDamage * abilityModifiers.magnitudeMult;
            float maxDamage = _projectile.maxDamage * abilityModifiers.magnitudeMult;

            var pattern1 = @"%m1";
            var pattern2 = @"%m2";

            var realDescription = Regex.Replace(_description, pattern1, minDamage.ToString());
            return Regex.Replace(realDescription, pattern2, maxDamage.ToString());
        }

    }
}