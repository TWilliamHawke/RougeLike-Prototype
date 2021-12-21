using System;

namespace Entities.Combat
{
    public static class DamageCalulator
    {
		static Random _rng;

		static DamageCalulator()
		{
			_rng = new Random();
		}

        public static int GetDamage(IDamageSource damageSource, IAttackTarget target)
        {
            float baseDamage = _rng.Next(damageSource.minDamage, damageSource.maxDamage);
            if(baseDamage == 0) return 0;

            float resist = target.resists[damageSource.damageType];

            float finalDamage = baseDamage / (1 + resist / baseDamage);
            return (int)finalDamage;
        }


    }
}