using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


namespace Map.Generator
{
	public class MapGenerator
	{
		Tilemap _tileMap;
		GeneratorConfig _config;
		Vector3 _playerSpawnPos = Vector3.zero;

		public Vector3 playerSpawnPos => _playerSpawnPos;

        public MapGenerator(Tilemap tileMap, GeneratorConfig config)
        {
            _tileMap = tileMap;
            _config = config;
        }

        public void StartGeneration()
		{
			var generator = new DefaultGenerator(_config);
			var intMap = generator.Create2dArray();

			_playerSpawnPos = generator.GetSpawnPoint();

			for (int x = 0; x <= intMap.GetUpperBound(0); x++)
			{
				for(int y = 0; y <= intMap.GetUpperBound(1); y++)
				{
					var position = new Vector3Int(x,y,0);
					_tileMap.SetTile(position, _config.tiles[intMap[x,y]]);
				}
			}


			Debug.Log("array created");
		}

	}

	interface IGenerationAlgorithm
	{
		int[,] Create2dArray();
		int[] walkableTiles { get; }
		
	}
}