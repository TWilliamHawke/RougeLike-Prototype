using UnityEngine;

namespace Abilities
{
    public class MovementAbility : IAbility
    {
        public Sprite abilityIcon => throw new System.NotImplementedException();

        MovementAbilityTemplate _template;

        public MovementAbility(MovementAbilityTemplate template)
        {
            _template = template;
        }
    }
}