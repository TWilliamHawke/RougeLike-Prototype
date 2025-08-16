using System.Collections;
using System.Collections.Generic;
using Abilities;
using Entities;
using UnityEngine;

namespace Map
{
    public class TileNode : ITileClickData, IAbilityTarget, IPositionData
    {
        public static int maxNeightborDistance => 15;
        public bool isWalkable { get; init; }

        public TileNode parent { get; set; }
        public float targetDist { get; set; }
        public float startDist { get; set; }
        public Vector3Int intPosition { get; init; }

        List<IObstacleEntity> _entitiesInThisNode = new();

        //getters
        public IEnumerable<IObstacleEntity> entitiesOnTile => _entitiesInThisNode;
        public bool isEmpty => _entitiesInThisNode.Count == 0;
        public bool isWalkableAndEmpty => isWalkable && isEmpty;
        public float totalDist => targetDist + startDist;
        public Vector3 position => intPosition;
        public int x => intPosition.x;
        public int y => intPosition.y;


        public TileNode(int x, int y, bool isWalkableTile)
        {
            intPosition = new Vector3Int(x, y, 0);
            isWalkable = isWalkableTile;
        }


        public float GetDistanceFrom(TileNode node)
        {
            float deltaX = Mathf.Abs(node.x - x);
            float deltaY = Mathf.Abs(node.y - y);

            if (deltaX > deltaY)
            {
                return deltaY * 14 + (deltaX - deltaY) * 10;
            }
            else
            {
                return deltaX * 14 + (deltaY - deltaX) * 10;
            }
        }

        public void AddEntity(IObstacleEntity entity)
        {
            _entitiesInThisNode.Add(entity);
        }

        public void RemoveEntity(IObstacleEntity entity)
        {
            _entitiesInThisNode.Remove(entity);
        }

        public override string ToString()
        {
            return $"Node at [{x}, {y}]";
        }

        public T GetEntityComponent<T>() where T : IEntityComponent
        {
            return default;
        }
    }
}