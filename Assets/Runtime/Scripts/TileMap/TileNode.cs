using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class TileNode
    {
        protected Vector2Int _position;
        public bool _isWalkable;

        public TileNode parent { get; set; }
        public float targetDist { get; set; }
        public float startDist { get; set; }



        //getters
        public bool isWalkable => _isWalkable;
        public float totalDist => targetDist + startDist;
        public int x => _position.x;
        public int y => _position.y;
        public Vector2Int position2d => _position;
		
        public TileNode(int x, int y, bool isWalkable)
        {
            _position = new Vector2Int(x, y);
            _isWalkable = isWalkable;
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

        public override string ToString()
        {
            return $"Node at [{x}, {y}]";
        }

    }
}