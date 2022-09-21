using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map.Generator
{
    public class GeneratorConfig : MapGenerator
    {
        [SerializeField] int _seed;
        [SerializeField] int _maxWidth;
        [SerializeField] int _maxHeight;
		[SerializeField] TileBase[] _tiles;

        public int seed => _seed;
        public int maxWidth => _maxWidth;
        public int maxHeight => _maxHeight;
		public TileBase[] tiles => _tiles;

        public override LocationMapData StartGeneration(Tilemap tilemap)
		{
            var mapData = new LocationMapData();

			var generator = new DefaultGenerator(this);
			mapData.walkabilityMap = generator.Create2dArray();
            mapData.width = _maxWidth;
            mapData.height = _maxHeight;

			mapData.playerSpawnPos = generator.GetSpawnPoint();

			for (int x = 0; x <= mapData.walkabilityMap.GetUpperBound(0); x++)
			{
				for(int y = 0; y <= mapData.walkabilityMap.GetUpperBound(1); y++)
				{
					var position = new Vector3Int(x,y,0);
					tilemap.SetTile(position, _tiles[mapData.walkabilityMap[x,y]]);
				}
			}

            return mapData;
		}

    }
}