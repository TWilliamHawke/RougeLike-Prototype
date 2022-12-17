using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map
{
	public interface IObjectWithCenterZone
	{
        int centerZoneWidth { get; }
        int centerZoneHeight { get; }
        TileBase centerZoneTile { get; }
		bool centerZoneIsWalkable { get; }
	}
}


