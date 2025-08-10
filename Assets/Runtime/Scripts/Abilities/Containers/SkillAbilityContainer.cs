using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class SkillAbilityContainer : AbilityContainer
    {
        public override bool canBeUsed => throw new System.NotImplementedException();

        public override void UpdateAbilityButton(IAbilityCounterHandler handler)
        {
            throw new System.NotImplementedException();
        }

        public override void UseAbility(AbilityController controller)
        {
            throw new System.NotImplementedException();
        }
    }

}