using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class LocationMapData : MonoBehaviour
    {
        int[,] walkabilityMap { get; }
        Vector3Int playerSpawnPos { get; }
        public int width;
        public int height;
    }
}

