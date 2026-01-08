using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using Map.Actions;

namespace Map.Zones
{
    [CreateAssetMenu(fileName = "RandomEncounter", menuName = "Map/Templates/Random Encounter", order = 0)]
    public class EncounterTemplate : ScriptableObject, IMapZoneTemplate, IZoneWithCenterTiles, ISpawnZoneTemplate, ItaskData
    {
        [UseFileName]
        [SerializeField] LocalString _displayName;
        [SpritePreview]
        [SerializeField] Sprite _icon;
		[SerializeField] LocalString _encounterDescription;
        [SerializeField] LocalString _taskText;
        [SerializeField] TaskTemplate _taskTemplate;
        [SerializeField] MapActionTemplate[] _possibleActions;

        [SerializeField] Vector2Int _spawnZoneSize = new Vector2Int(5, 5);
        [SerializeField] Vector2Int _colliderSize = new Vector2Int(5, 5);

		[Header("Tiles")]
		[SerializeField] int _tilesWidth = 3;
		[SerializeField] int _tilesHeight = 3;
		[SerializeField] TileTemplate _centerTile;

        [SerializeField] EntitiesTable _entities;

        public string displayName => _displayName;
        public Sprite icon => _icon;

        public Vector2Int centerZoneSize => new Vector2Int(_tilesWidth, _tilesHeight);
        //public Vector2Int size => new Vector2Int(_width, _height);
        public bool centerZoneIsWalkable => _centerTile?.isWalkable ?? true;
        public TileTemplate centerZoneTile => _centerTile;
        public MapActionTemplate[] possibleActions => _possibleActions;
        public EntitiesTable enemies => _entities;
        public Vector2Int size => _colliderSize;
        public string taskText => _taskText;
        Vector2Int ISpawnZoneTemplate.size => _spawnZoneSize;

        public ITaskController CreateTaskController()
        {
            return _taskTemplate.CreateTask(this, _taskText);
        }

        public void FillSpawnQueue(ISpawnQueue spawnQueue, System.Random rng)
        {
            spawnQueue.AddToQueue(_entities, rng);
        }
    }
}