using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Combat
{
	public interface IAttackTarget
	{
	    void TakeDamage(int damage);
		Dictionary<DamageType, int> resists { get; }
		Transform transform { get; }
	}
}