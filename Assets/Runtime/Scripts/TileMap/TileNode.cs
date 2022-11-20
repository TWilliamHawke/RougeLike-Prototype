using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;

namespace Map
{
    public class TileNode
    {
        public bool isWalkable { get; init; }

        public TileNode parent { get; set; }
        public float targetDist { get; set; }
        public float startDist { get; set; }
        public Vector3Int position { get; init; }
        public IObstacleEntity entityInthisNode { get; set; }


        //getters
        public bool isWalkableOrOccupied => isWalkable && entityInthisNode is null;
        public float totalDist => targetDist + startDist;
        public int x => position.x;
        public int y => position.y;
		
        public TileNode(int x, int y, bool isWalkableTile)
        {
            position = new Vector3Int(x, y, 0);
            this.isWalkable = isWalkableTile;
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

        public void RemoveEntity()
        {
            entityInthisNode = null;
        }

        public override string ToString()
        {
            return $"Node at [{x}, {y}]";
        }

    }
}