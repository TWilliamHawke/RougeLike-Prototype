using System.Collections;
using System.Collections.Generic;
using Entities;
using Entities.Behavior;
using UnityEngine;

namespace Map
{
    public class TilesGrid
    {
        TileNode[,] _grid;
        PathFinder _pathFinder;

        public TilesGrid(LocationMapData mapData)
        {
            _grid = new TileNode[mapData.width, mapData.height];
            FillGrid(mapData.walkabilityMap);
            _pathFinder = new PathFinder(this);
        }

        public Stack<TileNode> FindPath(TileNode from, TileNode to)
        {
            return _pathFinder.FindPath(from, to);
        }

        public bool TryGetNode(Vector3Int pos, out TileNode node)
        {
            return TryGetNodeAt(pos.x, pos.y, out node);
        }

        public bool TryAddEntityToTile(IObstacleEntity entity)
        {
            var tilePos = entity.transform.position.ToTilePos();
            if(PositionInsideGrid(tilePos.x, tilePos.y))
            {
                var node = _grid[tilePos.x, tilePos.y];
                if (node.entityInthisNode is not null) return false;
                node.entityInthisNode = entity;
                return true;
            }

            return false;
        }


        public bool TryGetNodeAt(int x, int y, out TileNode node)
        {
            if (PositionInsideGrid(x, y))
            {
                node = _grid[x, y];
                return true;
            }
            else
            {
                node = _grid[0, 0];
                return false;
            }
        }

        public List<TileNode> GetEmptyNeighbors(TileNode node)
        {
            var neightBors = new List<TileNode>();

            for (int x = node.x - 1; x <= node.x + 1; x++)
            {
                for (int y = node.y - 1; y <= node.y + 1; y++)
                {
                    if (!PositionInsideGrid(x, y)) continue;
                    var neighborNode = _grid[x, y];
                    if (neighborNode == node) continue;
                    if (!neighborNode.isWalkableOrOccupied) continue;
                    neightBors.Add(neighborNode);
                }
            }

            return neightBors;
        }

        void FillGrid(int[,] intMap)
        {
            for (int x = 0; x <= intMap.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= intMap.GetUpperBound(1); y++)
                {
                    _grid[x, y] = new TileNode(x, y, intMap[x, y] == 1);
                }
            }
        }

        bool PositionInsideGrid(int x, int y)
        {
            return x >= 0 && x <= _grid.GetUpperBound(0) && y >= 0 && y <= _grid.GetUpperBound(1);
        }
    }
}