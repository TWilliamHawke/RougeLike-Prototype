using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public struct LocationMapData
    {
        public int[,] walkabilityMap;
        public Vector3Int playerSpawnPos;
        public int width;
        public int height;
    }
}

