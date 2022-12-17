using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using UnityEngine.Tilemaps;

namespace Map.Objects
{
	[CreateAssetMenu(fileName ="Site", menuName ="Map/Templates/Site")]
	public class SiteTemplate :ScriptableObject, IMapObjectTemplate, IObjectWithCenterZone
	{
        [UseFileName]
        [SerializeField] string _displayName;
        [SpritePreview]
        [SerializeField] Sprite _icon;
		[TextArea(3,5)]
		[SerializeField] string _interactionDescription;
        [SerializeField] MapActionTemplate[] _possibleActions;

	    [SerializeField] int _width = 5;
		[SerializeField] int _height = 5;
		[Header("Population")]
		[SerializeField] CreaturesTable _enemies;

		[Header("Tiles")]
		[SerializeField] int _tilesWidth = 3;
		[SerializeField] int _tilesHeight = 3;
		[SerializeField] TileBase _siteTile;
		[SerializeField] bool _tilesIsWalkable;

        public string displayName => _displayName;
        public Sprite icon => _icon;
        public IEnumerable<MapActionTemplate> possibleActions => _possibleActions;
        public int width => _width; 
        public int height => _height; 
        public CreaturesTable enemies => _enemies; 
        public int centerZoneWidth => _tilesWidth; 
        public int centerZoneHeight => _tilesHeight; 
        public TileBase centerZoneTile => _siteTile; 
        public bool centerZoneIsWalkable => _tilesIsWalkable; 
    }
}

