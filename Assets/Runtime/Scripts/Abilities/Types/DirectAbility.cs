using UnityEngine;

namespace Abilities
{
    public class DirectAbility : IAbility
    {
        public Sprite abilityIcon => throw new System.NotImplementedException();

        DirectAbilityTemplate _template;

        public DirectAbility(DirectAbilityTemplate template)
        {
            _template = template;
        }
    }
}