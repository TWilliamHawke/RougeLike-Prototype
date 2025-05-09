using UnityEngine;

namespace Abilities
{
    public class SelfAbility : IAbility
    {
        public Sprite abilityIcon => throw new System.NotImplementedException();

        SelfAbilityTemplate _template;

        public SelfAbility(SelfAbilityTemplate template)
        {
            _template = template;
        }
    }
}