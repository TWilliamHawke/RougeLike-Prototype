using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Abilities
{
    public class SkillAbilityContainer : AbilityContainer
    {
        public override bool canBeUsed => throw new System.NotImplementedException();
        protected override IAbility ability => _ability;
        
        IAbility _ability { get; init; }

        public override void UpdateAbilityButton(IAbilityCounterHandler handler)
        {
            throw new System.NotImplementedException();
        }

        public override void UseAbility(IAbilityTarget target)
        {
            throw new System.NotImplementedException();
        }
    }

}