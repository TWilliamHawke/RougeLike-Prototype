using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public abstract class AbilityTemplate : DisplayedObject
    {
        public abstract void SelectAbilityController(AbilityController controller);
        public abstract string GetDescription(AbilityModifiers abilityModifiers);
	}
}