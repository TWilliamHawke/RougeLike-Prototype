using UnityEngine;

namespace Entities.Combat
{
    public readonly struct DamageSource : IDamageSource
    {
        public DamageType damageType { get; init; }
        public int minDamage { get; init; }
        public int maxDamage { get; init; }

        public DamageSource(int minDamage, int maxDamage, DamageType damageType)
        {
            this.damageType = damageType;
            this.minDamage = minDamage;
            this.maxDamage = maxDamage;
        }
    }
}