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

	    public string displayName => _displayName;
		public Sprite icon => _icon;
		public int maxStackCount => _maxStackSize;
		public int value => _value;

		

	}
}