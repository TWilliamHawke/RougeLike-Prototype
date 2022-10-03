using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using UnityEngine.Tilemaps;

namespace Map.Objects
{
	[CreateAssetMenu(fileName ="SiteTemplate", menuName ="Map/Templates/Site Template")]
	public class SiteTemplate : MapObjectTemplate
	{
	    [SerializeField] int _width = 5;
		[SerializeField] int _height = 5;
		[Header("Population")]
		[SerializeField] CreaturesTable _enemies;

		[Header("Tiles")]
		[SerializeField] int _tilesWidth = 3;
		[SerializeField] int _tilesHeight = 3;
		[SerializeField] TileBase _siteTile;
		[SerializeField] bool _tilesIsWalkable;

        public int width => _width; 
        public int height => _height; 
        public CreaturesTable enemies => _enemies; 
        public int tilesWidth => _tilesWidth; 
        public int tilesHeight => _tilesHeight; 
        public TileBase siteTile => _siteTile; 
        public bool tilesIsWalkable => _tilesIsWalkable; 
    }
}

