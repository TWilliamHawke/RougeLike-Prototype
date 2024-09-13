using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Entities.NPC;
using Entities;
using Map.Actions;

namespace Map.Zones
{
    [CreateAssetMenu(fileName = "RandomEncounter", menuName = "Map/Templates/Random Encounter", order = 0)]
    public class EncounterTemplate : ScriptableObject, IMapZoneTemplate, IZoneWithCenterTiles, ISpawnZoneTemplate
    {
        [UseFileName]
        [SerializeField] string _displayName;
        [SpritePreview]
        [SerializeField] Sprite _icon;
		[TextArea(3,5)]
		[SerializeField] string _interactionDescription;
        [SerializeField] MapActionTemplate[] _possibleActions;

        [SerializeField] Vector2Int _spawnZoneSize = new Vector2Int(5, 5);
        [SerializeField] Vector2Int _colliderSize = new Vector2Int(5, 5);

		[Header("Tiles")]
		[SerializeField] int _tilesWidth = 3;
		[SerializeField] int _tilesHeight = 3;
		[SerializeField] TileBase _centerTile;
		[SerializeField] bool _tilesIsWalkable = true;

        [SerializeField] EntitiesTable _entities;

        public string displayName => _displayName;
        public Sprite icon => _icon;

        public Vector2Int centerZoneSize => new Vector2Int(_tilesWidth, _tilesHeight);
        //public Vector2Int size => new Vector2Int(_width, _height);
        public bool centerZoneIsWalkable => _tilesIsWalkable;
        public TileBase centerZoneTile => _centerTile;

        public MapActionTemplate[] possibleActions => _possibleActions;
        public EntitiesTable enemies => _entities;

        public Vector2Int size => _colliderSize;
        Vector2Int ISpawnZoneTemplate.size => _spawnZoneSize;
    }
}