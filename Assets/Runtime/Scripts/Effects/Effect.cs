using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
	public class Effect : ScriptableObject
	{
		[UseFileName]
	    [SerializeField] string _displayName;
		[Multiline(4)]
		[SerializeField] string _description;
		[SpritePreview]
		[SerializeField] Sprite _icon;

		public Sprite icon => _icon;
	}
}