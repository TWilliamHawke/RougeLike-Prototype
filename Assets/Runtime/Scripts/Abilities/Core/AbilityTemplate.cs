using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public abstract class AbilityTemplate : DisplayedObject
    {
        [SerializeField] Injector _abilityController;
        
        protected Injector abilityController => _abilityController;

        public abstract void SelectAbilityController(AbilityController controller);
        public abstract string GetDescription(AbilityModifiers abilityModifiers);
        public abstract IAbility CreateAbility(IAbilityUser user);
	}
}