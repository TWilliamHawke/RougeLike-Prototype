using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
	public interface IAbilitySource
	{
	    IAbilityContainer CreateAbilityInstruction(AbilitiesFactory factory);
	}
}