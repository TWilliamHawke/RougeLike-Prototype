using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class SkillAbilityContainer : IAbilityContainer
    {
        public bool canBeUsed => throw new System.NotImplementedException();
        public string displayName => throw new System.NotImplementedException();
        public Sprite icon => throw new System.NotImplementedException();

        public void UpdateAbilityButton(IAbilityCounterHandler handler)
        {
            throw new System.NotImplementedException();
        }

        public void UseAbility(AbilityController controller)
        {
            throw new System.NotImplementedException();
        }
    }

}