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
    public class TileMapManager : MonoBehaviour
    {
        [SerializeField] Tilemap _tileMap;
        [SerializeField] Player _player;
        [SerializeField] MapObjectsManager _mapObjectsManager;

        [SerializeField] Location location;

        [SerializeField] Injector _tileGridInjector;

        TilesGrid _grid;

        public void StartUp()
        {
            _mapObjectsManager.SetLocation(location);
            // _generator = new MapGenerator(_tileMap, _config);
            // _generator.StartGeneration();
            var mapData = location.Create(_tileMap);

            _grid = new TilesGrid(mapData);
            _tileGridInjector.AddDependency(_grid);


            if (_grid.TryGetNodeAt(mapData.playerSpawnPos.x, mapData.playerSpawnPos.y, out var node))
            {
                _player.SpawnAt(node);
            }

        }

    }
}