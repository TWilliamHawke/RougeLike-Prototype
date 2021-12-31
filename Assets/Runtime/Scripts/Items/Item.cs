using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	public abstract class Item : ScriptableObject
	{
		[UseFileName]
	    [SerializeField] string _displayName;
		[SpritePreview]
		[SerializeField] Sprite _icon;
		[SerializeField] int _maxStackSize = 1;
		[SerializeField] int _value;
		[SerializeField] ItemSoundKit _soundKit;

	    public string displayName => _displayName;
		public Sprite icon => _icon;
		public int maxStackCount => _maxStackSize;
		public int value => _value;
		public AudioClip useSound => _soundKit.useSound;
		public AudioClip dragSound => _soundKit.dragSound;

		

	}
}