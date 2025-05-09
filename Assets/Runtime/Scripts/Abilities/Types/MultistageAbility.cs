using UnityEngine;

namespace Abilities
{
    public class MultistageAbility : IAbility
    {
        public Sprite abilityIcon => throw new System.NotImplementedException();

        MultistageAbilityTemplate _template;

        public MultistageAbility(MultistageAbilityTemplate template)
        {
            _template = template;
        }
    }
}