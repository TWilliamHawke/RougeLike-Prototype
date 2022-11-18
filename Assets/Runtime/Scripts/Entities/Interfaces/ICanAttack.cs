using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Combat
{
	public interface ICanAttack
	{
	    IDamageSource damageSource { get; }
		void PlayAttackSound();
	}
}