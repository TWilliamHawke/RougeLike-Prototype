using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class SummonAbility : IAbility
    {
        public Sprite abilityIcon => throw new System.NotImplementedException();

        SummonAbilityTemplate _template;

        public SummonAbility(SummonAbilityTemplate template)
        {
            _template = template;
        }
    }
}