using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Map.Generator;
using Entities.Player;
using Entities.Behavior;

namespace Map
{
    public class TileMapManager : MonoBehaviour, INeedInit
    {
        [SerializeField] Tilemap _tileMap;
        [SerializeField] GeneratorConfig _config;
        [SerializeField] PlayerCore _player;
        [SerializeField] TilemapController _tilemapController;
        [SerializeField] RoadConfig _pathConfig;

        IMapGenerator _generator;

        public void StartUp()
        {
            PathFinder.Init(_tilemapController);
            // _generator = new MapGenerator(_tileMap, _config);
            // _generator.StartGeneration();
            _generator = new RoadGenerator(_pathConfig, _tileMap);
            _generator.StartGeneration();

            _tilemapController.CreateGrid(_generator.mapSize, _generator.intMap);

            if (_tilemapController.TryGetNode(_generator.playerSpawnPos.x, _generator.playerSpawnPos.y, out var node))
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
        int[,] intMap { get; }
        Vector3Int playerSpawnPos { get; }
        MapSize mapSize { get; }
    }
}