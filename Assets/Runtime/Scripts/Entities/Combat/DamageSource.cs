using UnityEngine;

namespace Entities.Combat
{
    public readonly struct DamageSource : IDamageSource
    {
        public DamageType damageType { get; }
        public int minDamage { get; }
        public int maxDamage { get; }

        public DamageSource(int minDamage, int maxDamage, DamageType damageType)
        {
            this.damageType = damageType;
            this.minDamage = minDamage;
            this.maxDamage = maxDamage;
        }
    }
}