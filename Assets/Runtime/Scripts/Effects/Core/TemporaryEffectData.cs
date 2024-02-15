using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
	public class TemporaryEffectData
	{
	    public Effect effect { get; private set; }
		public int magnitude { get; private set; }
		public int remainingDuration { get; private set; }
		public IEffectSource effectSource { get; private set; }

		public TemporaryEffectData(SourceEffectData sourceEffectData)
		{
			UpdateEffectData(sourceEffectData);
		}

		public void UpdateEffectData(SourceEffectData sourceEffectData)
		{
			effect = sourceEffectData.effect;
			magnitude = sourceEffectData.power;
			remainingDuration = sourceEffectData.duration;
			effectSource = sourceEffectData.source;
		}
	}
}