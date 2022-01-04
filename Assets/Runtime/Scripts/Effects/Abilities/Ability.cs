using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
	public abstract class Ability : ScriptableObject
	{
		[UseFileName]
		[SerializeField] string _displayName;
	    public abstract void SelectControllerUsage(AbilityController controller);
	}
}