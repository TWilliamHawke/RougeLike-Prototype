using System.Collections;
using System.Collections.Generic;
using Map.Objects;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map.Generator
{
	[CreateAssetMenu(fileName ="RoadConfig", menuName="Musc/Road Config")]
	public class RoadConfig : ScriptableObject
	{
		[SerializeField] int _seed = 15;
		[Header("Size")]
	    [SerializeField] int _minLength = 100;
		[SerializeField] int _maxLength = 150;
		[SerializeField] int _roadWidth = 5;
		[SerializeField] int _emptyWidth = 5;
		[SerializeField] int _siteWidth = 10;
		[SerializeField] int _borderWidth = 2;
		[SerializeField] int _voidWidth = 2;
		[Header("Generation")]
		[Range(0,1)]
		[SerializeField] float _obstacleChance = .1f;
		[Range(0,1)]
		[SerializeField] float _randomTileChance = .1f;
		[SerializeField] int _minDistanceBetweenSites = 10;
		[SerializeField] int _maxDistanceBetweenSites = 15;
		[SerializeField] int _roadCurvesCount = 6;
		[Header("Tiles")]
		[SerializeField] TileBase _defaultTile;
		[SerializeField] TileBase _borderTile;
		[SerializeField] TileBase _roadTile;
		[SerializeField] TileBase[] _randomTiles;
		[SerializeField] TileBase[] _randomObstacles;
		[Header("Objects")]
		[SerializeField] Site _sitePrefab;
		[SerializeField] List<SiteTemplate> _siteTemplates;

        public int minLength => _minLength;
        public int maxLength => _maxLength;
        public int roadWidth => _roadWidth;
        public int emptyWidth => _emptyWidth;
        public int siteWidth => _siteWidth;
        public int borderWidth => _borderWidth;
        public int voidWidth => _voidWidth;

        public int totalWidth => (emptyWidth + siteWidth + borderWidth + voidWidth) * 2 + roadWidth;

        public int seed => _seed;
        public TileBase defaultTile => _defaultTile;
        public TileBase borderTile => _borderTile;
        public TileBase roadTile => _roadTile;
        public int roadCurvesCount => _roadCurvesCount;

        public Site sitePrefab => _sitePrefab;
        public List<SiteTemplate> siteTemplates => _siteTemplates;
        public int minDistanceBetweenSites => _minDistanceBetweenSites;
        public int maxDistanceBetweenSites => _maxDistanceBetweenSites;
        public float ObstacleChance => _obstacleChance;
        public float RandomTileChance => _randomTileChance;
    }
}

