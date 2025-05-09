using UnityEngine;

namespace Abilities
{
    public class AoeAbility : IAbility
    {
        public Sprite abilityIcon => throw new System.NotImplementedException();

        AoeAbilityTemplate _template;

        public AoeAbility(AoeAbilityTemplate template)
        {
            _template = template;
        }
    }
}