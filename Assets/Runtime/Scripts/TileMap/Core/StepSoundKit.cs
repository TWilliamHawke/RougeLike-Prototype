using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
	[CreateAssetMenu(fileName ="StepSoundKit", menuName = "Map/Steps SoundKit")]
	public class StepSoundKit : ScriptableObject
	{
		[SerializeField] StepSoundType _type;
		[SerializeField] AudioClip[] _stepSounds;

		public AudioClip GetRandom()
		{
			return _stepSounds.GetRandom();
		}
	}

	public enum StepSoundType
	{
		Default = 0
	}
}


