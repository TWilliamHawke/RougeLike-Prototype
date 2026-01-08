using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using Map.Actions;

namespace Map.Zones
{
	[CreateAssetMenu(fileName ="Site", menuName ="Map/Templates/Site")]
	public class SiteTemplate : ScriptableObject, IMapZoneTemplate, IZoneWithCenterTiles, ISpawnZoneTemplate, ItaskData
	{
        [UseFileName]
        [SerializeField] LocalString _displayName;
        [SpritePreview]
        [SerializeField] Sprite _icon;
		[SerializeField] LocalString _interactionDescription;
        [SerializeField] MapActionTemplate[] _possibleActions;
        [SerializeField] TaskTemplate _taskTemplate;
        [SerializeField] LocalString _taskText;

	    [SerializeField] int _width = 5;
		[SerializeField] int _height = 5;
		[Header("Population")]
		[SerializeField] EntitiesTable _enemies;

		[Header("Tiles")]
		[SerializeField] int _tilesWidth = 3;
		[SerializeField] int _tilesHeight = 3;
		[SerializeField] TileTemplate _siteTile;

        public string displayName => _displayName;
        public Sprite icon => _icon;
        public IEnumerable<MapActionTemplate> possibleActions => _possibleActions;
        public EntitiesTable enemies => _enemies; 
        public Vector2Int size => new Vector2Int(_width, _height);
        public Vector2Int centerZoneSize => new Vector2Int(_tilesWidth, _tilesHeight);
        public TileTemplate centerZoneTile => _siteTile; 
        public bool centerZoneIsWalkable => _siteTile?.isWalkable ?? true;
        public string taskText => _taskText;

        public ITaskController CreateTaskController()
        {
            return _taskTemplate.CreateTask(this, _taskText);
        }

        public void FillSpawnQueue(ISpawnQueue spawnQueue, System.Random rng)
        {
            spawnQueue.AddToQueue(_enemies, rng);
        }
    }
}

