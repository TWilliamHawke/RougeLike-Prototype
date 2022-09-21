using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Map.Generator;
using Entities.Player;
using Entities.Behavior;
using Map.Locations;

namespace Map
{
    public class TileMapManager : MonoBehaviour, INeedInit
    {
        [SerializeField] Tilemap _tileMap;
        [SerializeField] PlayerCore _player;
        [SerializeField] TilemapController _tilemapController;

        [SerializeField] Location location;

        public void StartUp()
        {
            PathFinder.Init(_tilemapController);
            // _generator = new MapGenerator(_tileMap, _config);
            // _generator.StartGeneration();
            var mapData = location.Create(_tileMap);

            _tilemapController.CreateGrid(mapData);

            if (_tilemapController.TryGetNode(mapData.playerSpawnPos.x, mapData.playerSpawnPos.y, out var node))
            {
                _player.SpawnAt(node);
            }

        }

        private void OnValidate()
        {
            UnityEditor.EditorApplication.delayCall += OnValidateCallback;
        }

        private void OnValidateCallback()
        {
            if (this is null)
            {
                UnityEditor.EditorApplication.delayCall -= OnValidateCallback;
                return; // MissingRefException if managed in the editor - uses the overloaded Unity == operator.
            }

            // _generator = new RoadGenerator(_pathConfig, _tileMap);
            // _generator.StartGeneration();

        }

    }

    public interface IMapGenerator
    {
        void StartGeneration();
        int[,] walkabilityMap { get; }
        Vector3Int playerSpawnPos { get; }
        MapSize mapSize { get; }
    }
}