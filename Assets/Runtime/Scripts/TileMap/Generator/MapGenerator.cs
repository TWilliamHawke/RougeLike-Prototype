using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Map.Generator
{
	public class MapGenerator: IMapGenerator
	{
		Tilemap _tileMap;
		GeneratorConfig _config;
		Vector3Int _playerSpawnPos = Vector3Int.zero;
		int[,] _intMap;

		public int[,] walkabilityMap => _intMap;

		public Vector3Int playerSpawnPos => _playerSpawnPos;

        public MapSize mapSize => _mapSize;
		MapSize _mapSize;

        public MapGenerator(Tilemap tileMap, GeneratorConfig config)
        {
            _tileMap = tileMap;
            _config = config;
			_mapSize = new MapSize(config.maxWidth, config.maxHeight);
        }

        public void StartGeneration()
		{
			var generator = new DefaultGenerator(_config);
			_intMap = generator.Create2dArray();

			_playerSpawnPos = generator.GetSpawnPoint();

			for (int x = 0; x <= _intMap.GetUpperBound(0); x++)
			{
				for(int y = 0; y <= _intMap.GetUpperBound(1); y++)
				{
					var position = new Vector3Int(x,y,0);
					_tileMap.SetTile(position, _config.tiles[_intMap[x,y]]);
				}
			}

		}

	}

	interface IGenerationAlgorithm
	{
		int[,] Create2dArray();
		int[] walkableTiles { get; }
		
	}
}