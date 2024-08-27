using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;

namespace Abilities
{
	public interface IAbilityWithTarget
	{
		bool TargetIsValid(IAbilityTarget target);
		void UseOnTarget(AbilityController controller, IAbilityTarget target);
	}
}