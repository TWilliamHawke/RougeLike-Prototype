using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using UnityEngine;

namespace Abilities
{
	public struct AbilityModifiers
	{
	    public float magnitudeMult { get; init; }
        public int durationAdd { get; init; }
		public DamageType newDamageType { get; init; }

        public AbilityModifiers(float magnitudeMult) : this()
        {
            this.magnitudeMult = magnitudeMult;
        }

        public AbilityModifiers(float magnitudeMult, DamageType newDamageType) : this()
        {
            this.magnitudeMult = magnitudeMult;
            this.newDamageType = newDamageType;
        }

        public AbilityModifiers(float magnitudeMult, int durationAdd)
        {
            this.magnitudeMult = magnitudeMult;
            this.durationAdd = durationAdd;
			newDamageType = DamageType.none;
        }

        public AbilityModifiers(float magnitudeMult, int durationAdd, DamageType newDamageType)
        {
            this.magnitudeMult = magnitudeMult;
            this.durationAdd = durationAdd;
            this.newDamageType = newDamageType;
        }
    }
}