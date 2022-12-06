using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using UnityEngine;

namespace Entities
{
    [CreateAssetMenu(fileName = "NPCTemplate", menuName = "Entities/NPCTemplate")]
    public class NPCTemplate : EntityTemplate
    {
		[UseFileName]
		[SerializeField] string _npcName;

        public override int minDamage => throw new System.NotImplementedException();
        public override int maxDamage => throw new System.NotImplementedException();
        public override DamageType damageType => throw new System.NotImplementedException();
    }
}


