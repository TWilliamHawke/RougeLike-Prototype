using System;

namespace Entities.Combat
{
    public static class DamagecalCulator
    {
		static Random _rng;

		static DamagecalCulator()
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