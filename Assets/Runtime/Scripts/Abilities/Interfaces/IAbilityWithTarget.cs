using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
	public interface IAbilityWithTarget
	{
		bool TargetIsValid(IAbilityTarget target);
		void UseOnTarget(AbilityController controller, IAbilityTarget target);
	}
}