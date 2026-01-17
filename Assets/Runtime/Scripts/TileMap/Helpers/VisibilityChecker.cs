using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Map.Helpers
{
    public class VisibilityChecker
    {
        TilesGrid _grid;
        Stack<TileNode> _nodes = new();
        TileNode _targetNode;

        List<Quadrant> _quadrantVectors = new()
        {
            new Quadrant(){ first = Vector2.up, second = Vector2.right },
            new Quadrant(){ first = Vector2.right, second = Vector2.down },
            new Quadrant(){ first = Vector2.down, second = Vector2.left },
            new Quadrant(){ first = Vector2.left, second = Vector2.up },
        };

        public VisibilityChecker(TilesGrid grid)
        {
            _grid = grid;
        }

        public bool HasVisibilityBetween(TileNode from, TileNode to)
        {
            var nodes = GetNodesBetween(from, to);

            foreach (var node in nodes)
            {
                if (!node.blockVision) return false;
            }
            return true;
        }

        public Stack<TileNode> GetNodesBetween(TileNode from, TileNode to)
        {
            _targetNode = to;
            _nodes.Clear();
            _nodes.Push(from);
            Vector3 rawDirection = to.intPosition - from.intPosition;

            Vector3 direction = rawDirection.normalized;
            Quadrant quadrant = _quadrantVectors
                .Aggregate((agg, next) => agg.Dot(direction) > next.Dot(direction) ? agg : next);

            AddNextNode(from.position, quadrant, direction);

            return _nodes;
        }

        private void AddNextNode(Vector3 startPoint, Quadrant quadrant, Vector3 direction)
        {
            TileNode node = _nodes.First();
            Vector3 cornerPos = node.position + quadrant.sum.AddZ(0) * 0.5f;
            Vector3 cornerDirection = cornerPos - startPoint;
            Vector3 nextNodeDirection = GetNextNodeDirection(cornerDirection, quadrant, direction);
            Vector3Int nextNodePosition = node.intPosition + nextNodeDirection.ToInt();
            if(nextNodePosition == _targetNode.intPosition) return;

            float borderDistance = Vector2.Dot(cornerDirection, nextNodeDirection);

            Vector2 directionX = new(direction.x, 0);
            Vector2 directionY = new(0, direction.y);
            float dotX = Vector2.Dot(nextNodeDirection, directionX);
            float dotY = Vector2.Dot(nextNodeDirection, directionY);
            Vector3 normDirection = direction.normalized;

            float normDistance = dotX > dotY ? normDirection.x : normDirection.y;
            Vector3 toNextPoint = normDirection * borderDistance / normDistance;
            Vector3 nextStartPoint = startPoint + toNextPoint;

            if (_grid.TryGetNode(nextNodePosition, out var nextNode))
            {
                _nodes.Push(nextNode);
                AddNextNode(nextStartPoint, quadrant, direction);
            }
        }

        private Vector3 GetNextNodeDirection(Vector3 cornerDirection, Quadrant quadrant, Vector3 direction)
        {
            float cross = Vector3.Cross(cornerDirection, direction).z;

            if (cross > 0)
            {
                return quadrant.first;
            }
            else if (cross < 0)
            {
                return quadrant.second;
            }
            else
            {
                return quadrant.sum;
            }
        }

        public struct Quadrant
        {
            public Vector2 first { get; init; }
            public Vector2 second { get; init; }

            public Vector2 sum => first + second;

            public float Dot(Vector2 other)
            {
                return Vector2.Dot(sum, other);
            }
        }


    }
}