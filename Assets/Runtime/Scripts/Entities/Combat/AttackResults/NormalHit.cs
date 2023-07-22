using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Combat
{
    public class NormalHit : IAttackResult
    {
        public int CalculateProbability(IDamageSource damageSource, IAttackTarget target)
        {
            return 1;
        }

        public void DoHit(IDamageSource damageSource, IAttackTarget target)
        {
            var damage = AttackHandler.GetDamage(damageSource, target);
            target.TakeDamage(damage);
        }
    }
}
