using UnityEngine;

namespace Entities.Combat
{
	public interface IDamageSource
	{
		int minDamage { get; }
		int maxDamage { get; }
	    DamageType damageType { get; }
	}
}