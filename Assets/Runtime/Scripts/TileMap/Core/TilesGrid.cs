using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using Map.Helpers;

namespace Map
{
    public class TilesGrid : IObserver<Entity>, IMapNodeStorage
    {
        TileNode[,] _grid;
        PathFinder _pathFinder;
        Vector2Int _gridSize;
        VisibilityChecker _visibilityChecker;

        public Vector2Int gridSize => _gridSize;

        public TilesGrid(LocationMapData mapData)
        {
            _grid = new TileNode[mapData.width, mapData.height];
            _gridSize = new Vector2Int(mapData.width, mapData.height);
            FillGrid(mapData.tiles);
            _pathFinder = new(this);
            _visibilityChecker = new(this);
        }

        public Stack<TileNode> FindPath(TileNode from, TileNode to)
        {
            return _pathFinder.FindPath(from, to);
        }

        public bool NodesHasVisibility(TileNode from, TileNode to)
        {
            return _visibilityChecker.HasVisibilityBetween(from, to);
        }

        public Stack<TileNode> FindPath(Vector3Int posFrom, Vector3Int posTo)
        {
            bool foundFrom = TryGetNode(posFrom, out var from);
            bool foundTo = TryGetNode(posTo, out var to);
            if (!foundFrom || !foundTo) return new Stack<TileNode>();
            return FindPath(from, to);
        }

        public bool TryGetNode(Vector3Int pos, out TileNode node)
        {
            return TryGetNodeAt(pos.x, pos.y, out node);
        }

        public bool TryGetNodeAt(int x, int y, out TileNode node)
        {
            bool insideGrid = _grid.IndexIsInsideBounds(x, y);
            node = insideGrid ? _grid[x, y] : _grid[0, 0];
            return insideGrid;
        }

        public TileNode GetNode(Vector3Int pos)
        {
            bool insideGrid = _grid.IndexIsInsideBounds(pos.x, pos.y);
            var node = insideGrid ? _grid[pos.x, pos.y] : _grid[0, 0];
            return node;
        }

        public List<TileNode> GetEmptyNeighbors(TileNode node)
        {
            var neightBors = new List<TileNode>();

            for (int x = node.x - 1; x <= node.x + 1; x++)
            {
                for (int y = node.y - 1; y <= node.y + 1; y++)
                {
                    if (!_grid.IndexIsInsideBounds(x, y)) continue;
                    var neighborNode = _grid[x, y];
                    if (neighborNode == node) continue;
                    if (!neighborNode.isWalkableAndEmpty) continue;
                    neightBors.Add(neighborNode);
                }
            }

            return neightBors;
        }

        public List<TileNode> GetNonEmptyNeighbors(Vector3Int position, int radius = 1)
        {
            var neightBors = new List<TileNode>();

            for (int x = position.x - radius; x <= position.x + radius; x++)
            {
                for (int y = position.y - radius; y <= position.y + radius; y++)
                {
                    if (!_grid.IndexIsInsideBounds(x, y)) continue;
                    var neighborNode = _grid[x, y];
                    if (neighborNode.intPosition == position) continue;
                    if (neighborNode.isEmpty) continue;
                    neightBors.Add(neighborNode);
                }
            }

            return neightBors;
        }

        bool TryAddEntityToTile(Entity entity)
        {
            var tilePos = entity.transform.position.ToTilePos();
            if (_grid.IndexIsInsideBounds(tilePos.x, tilePos.y))
            {
                var node = _grid[tilePos.x, tilePos.y];
                if (!node.isEmpty) return false;
                node.AddEntity(entity);
                return true;
            }

            return false;
        }

        void FillGrid(TileTemplate[,] tiles)
        {
            for (int x = 0; x <= tiles.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= tiles.GetUpperBound(1); y++)
                {
                    _grid[x, y] = new TileNode(x, y, tiles[x, y]);
                }
            }
        }

        void IObserver<Entity>.AddToObserve(Entity target)
        {
            TryAddEntityToTile(target);
        }

        void IObserver<Entity>.RemoveFromObserve(Entity target)
        {
        }
    }
}