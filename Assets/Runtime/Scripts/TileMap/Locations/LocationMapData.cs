using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class LocationMapData
    {
        public TileTemplate[,] tiles { get; init; }
        public Vector3Int playerSpawnPos { get; init; }
        public int width { get; init; }
        public int height { get; init; }
    }
}

