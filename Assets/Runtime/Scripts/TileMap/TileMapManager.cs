using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Map.Generator;
using Entities.PlayerScripts;

namespace Map
{
	public class TileMapManager : MonoBehaviour, INeedInit
	{
		[SerializeField] Tilemap _tileMap;
		[SerializeField] GeneratorConfig _config;
		[SerializeField] Player _player;
		[SerializeField] TilemapController _tilemapController;
		
		MapGenerator _generator;

        public void StartUp()
        {
            _generator = new MapGenerator(_tileMap, _config);
			_generator.StartGeneration();

			_tilemapController.CreateGrid(_config, _generator.intMap);

			if(_tilemapController.TryGetNode(_generator.playerSpawnPos.x, _generator.playerSpawnPos.y, out var node))
			{
				_player.SpawnAt(node);
			}

        }

		private void OnValidate() {
			_generator = new MapGenerator(_tileMap, _config);
			_generator.StartGeneration();
		}

	}
}