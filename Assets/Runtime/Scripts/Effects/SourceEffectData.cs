using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
	[System.Serializable]
	public class SourceEffectData
	{
	    [SerializeField] Effect _effect;
		[SerializeField] int _power;
		[SerializeField] int _duration;

		public void ApplyEffect(IEffectTarget target)
		{

		}

		public string EffectDescription()
		{
			return "Description";
		}
	}
}