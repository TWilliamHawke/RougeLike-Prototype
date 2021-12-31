using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	[CreateAssetMenu(fileName ="ItemSoundKit", menuName = "Items/ItemSoundKit")]
	public class ItemSoundKit : ScriptableObject
	{
	    [SerializeField] AudioClip[] _useSounds;
		[SerializeField] AudioClip[] _dragSounds;

		public AudioClip useSound => _useSounds.GetRandom();
		public AudioClip dragSound => _dragSounds.GetRandom();
	}
}