using UnityEngine;

namespace Entities.Combat
{
    public static class DamageCalulator
    {
		
        public static int GetDamage(IDamageSource damageSource, IAttackTarget target)
        {
            float baseDamage = Random.Range(damageSource.minDamage, damageSource.maxDamage);
            if(baseDamage < 1) return 0;
            int resist = 0;

            target.resists.TryGetValue(damageSource.damageType, out resist);

            float finalDamage = baseDamage / (1 + (float)resist / baseDamage);
            return (int)finalDamage;
        }


    }
}