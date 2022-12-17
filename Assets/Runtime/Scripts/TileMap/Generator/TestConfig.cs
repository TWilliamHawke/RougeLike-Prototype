using System.Collections;
using System.Collections.Generic;
using Map.Objects;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map.Generator
{
    public class TestConfig : GeneratorConfig
    {
        [SerializeField] int _seed;
        [SerializeField] int _maxWidth;
        [SerializeField] int _maxHeight;
		[SerializeField] TileBase[] _tiles;

        public int seed => _seed;
        public int maxWidth => _maxWidth;
        public int maxHeight => _maxHeight;
		public TileBase[] tiles => _tiles;

        public override IGenerationLogic GetLogic(Tilemap tilemap)
		{
			return new DefaultGenerator(this, tilemap);
		}

    }
}