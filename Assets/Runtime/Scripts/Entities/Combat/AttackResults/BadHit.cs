using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Combat
{
    public class BadHit : IAttackResult
    {
        public int CalculateProbability(IDamageSource damageSource, IAttackTarget target)
        {
            return 1;
        }

        public void DoHit(IDamageSource damageSource, IAttackTarget target)
        {
            var damage = AttackHandler.GetDamage(damageSource, target, .5f);
            target.TakeDamage(damage);
        }
    }
}
