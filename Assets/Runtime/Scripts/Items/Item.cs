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

	    public virtual string displayName => _displayName;
		public Sprite icon => _icon;
		public int maxStackSize => _maxStackSize;
		public virtual int value => _value;
		public AudioClip useSound => _soundKit.useSound;
		public AudioClip dragSound => _soundKit.dragSound;

		

	}
}