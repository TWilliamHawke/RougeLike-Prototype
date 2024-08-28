using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public abstract class Ability : ScriptableObject
	{
		[UseFileName]
		[SerializeField] string _displayName;

		public string displayName => _displayName;

	    public abstract void SelectControllerUsage(AbilityController controller);
		public abstract string GetDescription(AbilityModifiers abilityModifiers);
	}
}