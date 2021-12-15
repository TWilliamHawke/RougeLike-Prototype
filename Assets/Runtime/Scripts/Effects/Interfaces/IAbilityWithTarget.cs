using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
	public interface IAbilityWithTarget
	{
		bool TargetIsValid(IEffectTarget target);
		void UseOnTarget(AbilityController controller, IEffectTarget target);
	}
}