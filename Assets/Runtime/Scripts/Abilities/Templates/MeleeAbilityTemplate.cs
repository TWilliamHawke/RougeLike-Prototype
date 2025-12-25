using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Melee")]
    public class MeleeAbilityTemplate : AbilityTemplate, IEffectSource
    {
        [SerializeField] Injector _effectsHandler;
        [SerializeField] bool _useWeaponStats = true;
        [HideIf("_useWeaponStats", true)]
        [SerializeField] IntValue _damage;
        [HideIf("_useWeaponStats", true)]
		[SerializeField] DamageStoredResource _damageType;
        [HideIf("_useWeaponStats", false)]
        [Range(0f, 3f)]
        [SerializeField] float _damageMultiplier = 1f;

        protected bool useWeaponStats => _useWeaponStats;

        public override AbstractAbility CreateAbility()
        {
            MeleeAbility ability = new(this);
            ability.BindEffectSource(this);
            abilityController.AddInjectionTarget(ability);
            _effectsHandler.AddInjectionTarget(ability);
            return ability;
        }

        public IEnumerable<ISourceEffectData> GetEffects()
        {
            int damage = Mathf.RoundToInt(_damage * _damageMultiplier);
            yield return new SourceEffectData(_damageType, damage, 0);
        }
    }
}