using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map.Generator;

namespace Map
{
    public class TileGrid
    {
        TileNode[,] _grid;

        public TileGrid(MapSize mapSize, int[,] intMap)
        {
            _grid = new TileNode[mapSize.width, mapSize.height];
            FillGrid(intMap);
        }

        void FillGrid(int[,] intMap)
        {
            for (int x = 0; x <= intMap.GetUpperBound(0); x++)
            {
                for (int y = 0; y <= intMap.GetUpperBound(1); y++)
                {
					_grid[x,y] = new TileNode(x, y, intMap[x,y] == 1);
                }
            }

        }

        public bool TryGetNodeAt(int x, int y, out TileNode node)
        {
            if(PositionInsideGrid(x,y))
            {
                node = _grid[x,y];
                return true;
            }
            else
            {
                node = _grid[0,0];
                return false;
            }
        }

		public List<TileNode> GetNeighbors(TileNode node)
		{
			var neightBors = new List<TileNode>();

            for(int x = node.x -1; x <= node.x + 1; x++)
            {
                for(int y = node.y - 1; y <= node.y + 1; y++)
                {
                    if (!PositionInsideGrid(x, y)) continue;
                    var neighborNode = _grid[x,y];
                    if(neighborNode == node) continue;
                    if (!neighborNode.isWalkable) continue;
                    neightBors.Add(neighborNode);
                }
            }

			return neightBors;
		}


        bool PositionInsideGrid(int x, int y)
        {
            return x >= 0 && x <= _grid.GetUpperBound(0) && y >= 0 && y <= _grid.GetUpperBound(1);
        }
    }
}