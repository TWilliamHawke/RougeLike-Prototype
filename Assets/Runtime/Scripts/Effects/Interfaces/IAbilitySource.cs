using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
	public interface IAbilitySource
	{
	    void UseAbility(AbilityController controller);
		Sprite abilityIcon { get; }
	}
}