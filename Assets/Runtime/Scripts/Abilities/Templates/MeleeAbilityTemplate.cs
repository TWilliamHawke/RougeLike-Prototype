using System.Collections;
using System.Collections.Generic;
using Effects;
using Items;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Abilities/Melee")]
    public class MeleeAbilityTemplate : AbilityTemplate, IEffectSource
    {
        [SerializeField] Injector _effectsHandler;
        [SerializeField] ItemSoundKit _soundKit;
        [SerializeField] bool _useWeaponStats = true;
        [HideIf("_useWeaponStats", true)]
        [SerializeField] IntValue _damage;
        [HideIf("_useWeaponStats", true)]
		[SerializeField] DamageStoredResource _damageType;
        [HideIf("_useWeaponStats", false)]
        [Range(0f, 3f)]
        [SerializeField] float _damageMultiplier = 1f;
        [SerializeField] AttackPatterns _attackPatterns;

        protected bool useWeaponStats => _useWeaponStats;
        public AudioClip useSound => _soundKit.useSound;

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

        public bool HitTargetIsValid(Vector3 userPosition, Vector3 targetPosition)
        {
            var hitPosition = (targetPosition - userPosition).ToInt();
            return _attackPatterns.HitPositionIsValid(hitPosition);
        }

        public IEnumerable<Vector3Int> GetTargetPositions(Vector3 hitPosition)
        {
            return _attackPatterns.GetTargetPositions(hitPosition);
        }
    }
}