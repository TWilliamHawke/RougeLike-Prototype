using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map.Locations
{
	public abstract class Location : ScriptableObject
	{
	    [UseFileName] [SerializeField] string _displayName;
		[SpritePreview] [SerializeField] Sprite _icon;

		public abstract void Create(Tilemap tilemap);
	}
}

