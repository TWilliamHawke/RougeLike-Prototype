using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
	public class TemporaryEffectData : IStaticEffectData
	{
	    public IEffect effect { get; private set; }
		public int magnitude { get; private set; }
		public int remainingDuration { get; private set; }

        public IEffectSignature effectType => effect.effectType;
        public BonusValueType bonusType => effect.bonusType;

        public TemporaryEffectData(SourceEffectData sourceEffectData)
		{
			UpdateEffectData(sourceEffectData);
		}

		public void UpdateEffectData(SourceEffectData sourceEffectData)
		{
			effect = sourceEffectData.effect;
			magnitude = sourceEffectData.magnitude;
			remainingDuration = sourceEffectData.duration;
		}

		public bool IsPositive()
		{
			if (effect.isPositiveValueGood)
			{
				return magnitude > 0;
			}
			else
			{
				return magnitude < 0;
			}
		}
	}
}