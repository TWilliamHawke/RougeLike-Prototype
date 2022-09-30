using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Map.Generator;
using Entities.PlayerScripts;
using Entities.Behavior;
using Map.Locations;
using Map.Objects;

namespace Map
{
    public class TileMapManager : MonoBehaviour, INeedInit
    {
        [SerializeField] Tilemap _tileMap;
        [SerializeField] Player _player;
        [SerializeField] TilemapController _tilemapController;
        [SerializeField] MapObjectsManager _mapObjectsManager;

        [SerializeField] Location location;

        public void StartUp()
        {
            PathFinder.Init(_tilemapController);
            _mapObjectsManager.SetLocation(location);
            // _generator = new MapGenerator(_tileMap, _config);
            // _generator.StartGeneration();
            var mapData = location.Create(_tileMap);

            _tilemapController.CreateGrid(mapData);

            if (_tilemapController.TryGetNode(mapData.playerSpawnPos.x, mapData.playerSpawnPos.y, out var node))
            {
                _player.SpawnAt(node);
            }

        }

    }
}