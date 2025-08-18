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
        [SerializeField] IntValue _damage;
        [SerializeField] DamageStoredResource _damageType;
        [LocalisationKey]
        [SerializeField] string _description;

        public ProjectileTemplate projectile => _projectile;

        public override IAbility CreateAbility()
        {
            ProjectileAbility ability = new(this);
            abilityController.AddInjectionTarget(ability);
            return ability;
        }

        public IEnumerable<ISourceEffectData> GetEffects()
        {
            yield return new SourceEffectData(_damageType, _damage, 0);
        }

        public  string GetDescription(AbilityModifiers abilityModifiers)
        {
            float minDamage = _damage.minValue * abilityModifiers.magnitudeMult;
            float maxDamage = _damage.maxValue * abilityModifiers.magnitudeMult;

            var pattern1 = @"%m1";
            var pattern2 = @"%m2";

            var realDescription = Regex.Replace(_description, pattern1, minDamage.ToString());
            return Regex.Replace(realDescription, pattern2, maxDamage.ToString());
        }

    }
}