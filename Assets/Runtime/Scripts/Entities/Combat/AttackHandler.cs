using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Combat
{
    public static class AttackHandler
    {
        static List<IAttackResult> _attackResults = new()
            {
                new FailedHit(),
                new BadHit(),
                new NormalHit(),
                new CriticalHit(),
                new DeadlyHit(),
            };


        public static void ProcessAttack(IDamageSource damageSource, IAttackTarget attackTarget)
        {
            var result = _attackResults.GetRandonByWeight(
                hit => hit.CalculateProbability(damageSource, attackTarget));

            result.DoHit(damageSource, attackTarget);
        }

        public static int GetDamage(IDamageSource damageSource, IAttackTarget target, float damageMult = 1f)
        {
            float baseDamage = Random.Range(damageSource.minDamage, damageSource.maxDamage);
            baseDamage *= damageMult;
            if(baseDamage < 1) return 0;
            int resist = 0;

            target.resists.TryGetValue(damageSource.damageType, out resist);

            float finalDamage = baseDamage / (1 + (float)resist / baseDamage);
            return (int)finalDamage;
        }

    }
}
