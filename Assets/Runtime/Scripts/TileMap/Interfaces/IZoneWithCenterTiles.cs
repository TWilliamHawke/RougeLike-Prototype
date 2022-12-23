using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map
{
	public interface IZoneWithCenterTiles
	{
        Vector2Int centerZoneSize { get; }
        TileBase centerZoneTile { get; }
		bool centerZoneIsWalkable { get; }
	}
}


