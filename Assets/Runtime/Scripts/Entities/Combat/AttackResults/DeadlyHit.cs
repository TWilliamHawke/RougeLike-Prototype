using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Combat
{
    public class DeadlyHit : IAttackResult
    {
        public int CalculateProbability(IDamageSource damageSource, IAttackTarget target)
        {
            int resist = 0;
            target.resists.TryGetValue(damageSource.damageType, out resist);

            if (resist < 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        public void DoHit(IDamageSource damageSource, IAttackTarget target)
        {
            target.TakeDamage(9999999);
        }
    }
}
