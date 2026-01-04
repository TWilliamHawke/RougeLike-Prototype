using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
	public class TemporaryEffectData
	{
	    public IEffect effect { get; private set; }
		public int magnitude { get; private set; }
		public int remainingDuration { get; private set; }

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