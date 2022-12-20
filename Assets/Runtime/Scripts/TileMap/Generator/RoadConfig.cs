using System.Collections;
using System.Collections.Generic;
using Map.Zones;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map.Generator
{
	[CreateAssetMenu(fileName ="RoadConfig", menuName="Musc/Road Config")]
	public partial class RoadConfig : GeneratorConfig
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
		// [Range(0,1)] [SerializeField] float _obstacleChance = .1f;
		// [Range(0,1)] [SerializeField] float _randomTileChance = .1f;
		[SerializeField] int _minDistanceBetweenSites = 10;
		[SerializeField] int _maxDistanceBetweenSites = 15;
		[SerializeField] int _roadCurvesCount = 6;
		[Header("Tiles")]
		[SerializeField] TileBase _defaultTile;
		[SerializeField] TileBase _borderTile;
		[SerializeField] TileBase _roadTile;
		[SerializeField] TileBase[] _randomTiles;
		[SerializeField] TileBase[] _randomObstacles;
		[Header("Map Zones")]
		[SerializeField] List<SiteTemplate> _siteTemplates;
		[SerializeField] List<EncounterTemplate> _encounterTemplates;

        int totalWidth => (_emptyWidth + _siteWidth + _borderWidth + _voidWidth) * 2 + _roadWidth;
		

        public override IGenerationLogic GetLogic(Tilemap tilemap)
        {
            return new RoadGeneratorr(tilemap, this);
        }
    }
}

