using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Combat
{
    public class CriticalHit : IAttackResult
    {
        public int CalculateProbability(IDamageSource damageSource, IAttackTarget target)
        {
            return 1;
        }

        public void DoHit(IDamageSource damageSource, IAttackTarget target)
        {
            var damage = AttackHandler.GetDamage(damageSource, target, 1.5f);
            target.TakeDamage(damage);
        }
    }
}
