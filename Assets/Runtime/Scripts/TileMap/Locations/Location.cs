using System.Collections;
using System.Collections.Generic;
using Map.Objects;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map.Locations
{
	public abstract class Location : ScriptableObject
	{
	    [UseFileName] [SerializeField] string _displayName;
		[SpritePreview] [SerializeField] Sprite _icon;

		public abstract LocationMapData Create(Tilemap tilemap);

		public string displayName => _displayName;
		public Sprite icon => _icon;

		public abstract MapObjectTask task { get; }
	}
}

