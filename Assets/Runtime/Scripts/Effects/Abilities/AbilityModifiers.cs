using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using UnityEngine;

namespace Effects
{
	public struct AbilityModifiers
	{
	    public float magnitudeMult;
        public int durationAdd;
		public DamageType newDamageType;

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