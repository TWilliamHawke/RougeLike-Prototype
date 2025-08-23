using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Effects;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Projectile")]
    public class ProjectileAbilityTemplate : AbilityTemplate, IEffectSource
    {
        [SerializeField] Injector _effectsHandler;
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
            _effectsHandler.AddInjectionTarget(ability);
            return ability;
        }

        public IEnumerable<ISourceEffectData> GetEffects()
        {
            yield return new SourceEffectData(_damageType, _damage, 0);
        }

        public string GetDescription(AbilityModifiers abilityModifiers)
        {
            float minDamage = _damage.minValue * abilityModifiers.magnitudeMult;
            float maxDamage = _damage.maxValue * abilityModifiers.magnitudeMult;

            var pattern1 = @"%m1";
            var pattern2 = @"%m2";
            string description = LocalDictionary.GetLocalisedString(_description,
                new TextReplacer(pattern1, minDamage.ToString()),
                new TextReplacer(pattern2, maxDamage.ToString()));
            return description;
        }

    }
}