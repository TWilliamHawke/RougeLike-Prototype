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

		protected string displayName => _displayName;
		protected Sprite icon => _icon;

		public abstract TaskData task { get; }
	}
}

