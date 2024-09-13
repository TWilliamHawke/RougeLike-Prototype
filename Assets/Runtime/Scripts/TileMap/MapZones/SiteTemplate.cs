using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using UnityEngine.Tilemaps;
using Map.Actions;

namespace Map.Zones
{
	[CreateAssetMenu(fileName ="Site", menuName ="Map/Templates/Site")]
	public class SiteTemplate :ScriptableObject, IMapZoneTemplate, IZoneWithCenterTiles, ISpawnZoneTemplate
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
		[SerializeField] EntitiesTable _enemies;

		[Header("Tiles")]
		[SerializeField] int _tilesWidth = 3;
		[SerializeField] int _tilesHeight = 3;
		[SerializeField] TileBase _siteTile;
		[SerializeField] bool _tilesIsWalkable;

        public string displayName => _displayName;
        public Sprite icon => _icon;
        public IEnumerable<MapActionTemplate> possibleActions => _possibleActions;
        public EntitiesTable enemies => _enemies; 
        public Vector2Int size => new Vector2Int(_width, _height);
        public Vector2Int centerZoneSize => new Vector2Int(_tilesWidth, _tilesHeight);
        public TileBase centerZoneTile => _siteTile; 
        public bool centerZoneIsWalkable => _tilesIsWalkable;
    }
}

