using UnityEngine;

namespace Abilities
{
    public class MeleeAbility : IAbility
    {
        public Sprite abilityIcon => throw new System.NotImplementedException();

        MeleeAbilityTemplate _template;

        public MeleeAbility(MeleeAbilityTemplate template)
        {
            _template = template;
        }
    }
}