using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Map
{
    public interface ITileClickData
    {
        Vector3Int intPosition { get; }
        bool isWalkableAndEmpty { get; }
        IEnumerable<IObstacleEntity> entitiesOnTile { get; }
    }
}