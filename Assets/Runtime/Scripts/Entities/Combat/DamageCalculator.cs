using UnityEngine;

namespace Entities.Combat
{
    public static class DamageCalulator
    {
		
        public static int GetDamage(IDamageSource damageSource, IAttackTarget target)
        {
            float baseDamage = Random.Range(damageSource.minDamage, damageSource.maxDamage);
            if(baseDamage < 1) return 0;
            float resist = target.resists[damageSource.damageType];

            float finalDamage = baseDamage / (1 + resist / baseDamage);
            return (int)finalDamage;
        }


    }
}