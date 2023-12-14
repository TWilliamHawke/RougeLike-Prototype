using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Map.Generator;
using Entities.PlayerScripts;
using Map.Locations;
using Map.Zones;
using Entities;

namespace Map
{
    public class TileMapManager : MonoBehaviour
    {
        [SerializeField] Tilemap _tileMap;
        [SerializeField] Player _player;

        [SerializeField] Location _location;

        [SerializeField] Injector _tileGridInjector;

        [InjectField] EntitiesManager _entitiesManager;

        public Location location => _location;
        TilesGrid _grid;

        private void Awake()
        {
            var rawMapData = _location.Create(_tileMap);

            _grid = new TilesGrid(rawMapData);
            _tileGridInjector.SetDependency(_grid);


            if (_grid.TryGetNodeAt(rawMapData.playerSpawnPos.x, rawMapData.playerSpawnPos.y, out var node))
            {
                _player.SpawnAt(node);
            }
            else
            {
                throw new System.Exception("node for player spawn not found");
            }

        }

        //for editor
        public void StartObservation()
        {
            _entitiesManager.AddObserver(_grid);
        }
    }
}