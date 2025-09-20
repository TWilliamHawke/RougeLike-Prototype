using UnityEngine;

namespace Abilities
{
    public abstract class AbilityTemplate : DisplayedObject
    {
        [SerializeField] Injector _abilityController;

        protected Injector abilityController => _abilityController;

        public abstract IAbility CreateAbility();
    }
}