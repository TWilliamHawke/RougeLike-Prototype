using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Map.Generator;
using PlayerScripts;

namespace Map
{
	public class TileMapManager : MonoBehaviour, INeedInit
	{
		[SerializeField] Tilemap _tileMap;
		[SerializeField] GeneratorConfig _config;
		[SerializeField] Player _player;
		
		MapGenerator _generator;

        public void Init()
        {
            _generator = new MapGenerator(_tileMap, _config);
			_generator.StartGeneration();
			Debug.Log(_generator.playerSpawnPos);
			_player.TeleportTo(_generator.playerSpawnPos);

        }

		private void OnValidate() {
			_generator = new MapGenerator(_tileMap, _config);
			_generator.StartGeneration();
		}

	}
}