using UnityEngine;

namespace Entities.Combat
{
    public struct DamageSource : IDamageSource
    {
        public DamageType damageType => _damageType;
        public int minDamage => _minDamage;
        public int maxDamage => _maxDamage;
        public AudioClip[] attackSounds => _attackSounds;

        DamageType _damageType;
        int _minDamage;
        int _maxDamage;
		AudioClip[] _attackSounds;

        public DamageSource(int minDamage, int maxDamage, DamageType damageType, AudioClip[] attackSounds)
        {
            _damageType = damageType;
            _minDamage = minDamage;
            _maxDamage = maxDamage;
			_attackSounds = attackSounds;
        }
    }
}