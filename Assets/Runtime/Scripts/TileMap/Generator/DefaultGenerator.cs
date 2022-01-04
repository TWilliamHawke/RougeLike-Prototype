using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Rng = System.Random;

namespace Map.Generator
{
    public class DefaultGenerator : IGenerationAlgorithm
    {
        GeneratorConfig _config;
        Rng _rng;
        int[,] _intMap;
        CellInfo[,] _grid;

        CellInfo _startCell;

        const int CELL_SIZE = 15;
        const int CHANCE_CREATE_HALL = 30;
        const int CHANCE_CREATE_NON_PARENT_BRIDGE = 30;

        int _neightborsCount = 0;


        int[] _walkableTiles = new[] { 1 };

        public DefaultGenerator(GeneratorConfig config)
        {
            _config = config;
            _rng = new Rng(_config.seed);
        }

        public int[] walkableTiles => _walkableTiles;

        public int[,] Create2dArray()
        {
            _intMap = new int[_config.maxWidth, _config.maxHeight];

            CreateGrid();
            CreateBridges();
            ClearWalls();

            return _intMap;
        }

        public Vector3Int GetSpawnPoint()
        {
            int x = FindMiddleLine(_startCell.gridX);
            int y = FindMiddleLine(_startCell.gridY);
            return new Vector3Int(x, y, 0);
        }

        private void ClearWalls()
        {
            for (int x = 1; x < _intMap.GetUpperBound(0); x++)
            {
                for (int y = 1; y < _intMap.GetUpperBound(1); y++)
                {
                    int tileType = _intMap[x, y];

                    if (tileType != 2) continue; //wall check

                    //if left and right tiles are walkable
                    if (_intMap[x - 1, y] == 1 && _intMap[x + 1, y] == 1)
                    {
                        _intMap[x, y] = 1;
                    }
                    //if top and down tiles are walkable
                    if (_intMap[x, y - 1] == 1 && _intMap[x, y + 1] == 1)
                    {
                        _intMap[x, y] = 1;
                    }
                }
            }
        }

        private void CreateBridges()
        {
            // for (int x = 0; x < _grid.GetUpperBound(0); x++)
            // {
            //     for (int y = 0; y < _grid.GetUpperBound(1); y++)
            //     {
            //         //if(_rng.Next(1, 99) > 80) continue;

            //         var cell = _grid[x, y];
            //         if (cell is null) continue;

            //         var topCell = _grid[x, y + 1];
            //         if (topCell != null)
            //         {
            //             CreateBridgeBetweenCells(cell, topCell);
            //         }

            //         var rightCell = _grid[x + 1, y];
            //         if (rightCell != null)
            //         {
            //             CreateBridgeBetweenCells(cell, rightCell);
            //         }
            //     }
            // }

            foreach (var cell in _grid)
            {
                if (cell?.parent is null) continue;
                CreateBridgeBetweenCells(cell, cell.parent);
            }
        }

        private void CreateBridgeBetweenCells(CellInfo cell1, CellInfo cell2)
        {
            int x1 = Mathf.Max(Mathf.Min(cell1.x1, cell1.x2), Mathf.Min(cell2.x1, cell2.x2));
            int x2 = Mathf.Min(Mathf.Max(cell1.x1, cell1.x2), Mathf.Max(cell2.x1, cell2.x2));
            int y1 = Mathf.Max(Mathf.Min(cell1.y1, cell1.y2), Mathf.Min(cell2.y1, cell2.y2));
            int y2 = Mathf.Min(Mathf.Max(cell1.y1, cell1.y2), Mathf.Max(cell2.y1, cell2.y2));

            CreatePolygonFromTo(x1, x2, y1, y2);
        }

        private void CreateGrid()
        {
            int gridWidth = _config.maxWidth / CELL_SIZE;
            int gridHeight = _config.maxHeight / CELL_SIZE;

            _grid = new CellInfo[gridWidth, gridHeight];

            int startCellX = _rng.Next(0, gridWidth);
            int startCellY = _rng.Next(0, gridWidth);
            _startCell = CreateCell(startCellX, startCellY, null);

            CreateHall(ref _startCell); //first cell for spawn player


            CreateNeightbors(_startCell);

            for(int x = 0; x <= _grid.GetUpperBound(0); x++)
            {
                for(int y = 0; y <= _grid.GetUpperBound(1); y++)
                {
                    var cell = _grid[x,y];
                    if(cell is null) continue;

                    if(!cell.hasChildren)
                    {
                        CreateHall(ref cell);
                    }

                    CreatePolygon(cell);

                }
            }

        }

        private void CreateNeightbors(CellInfo cell)
        {
            if (cell is null) return;
            var cell1 = TryCreateNeightbor(cell.gridX, cell.gridY + 1, cell);
            var cell2 = TryCreateNeightbor(cell.gridX, cell.gridY - 1, cell);
            var cell3 = TryCreateNeightbor(cell.gridX + 1, cell.gridY, cell);
            var cell4 = TryCreateNeightbor(cell.gridX - 1, cell.gridY, cell);

            CreateNeightbors(cell1);
            CreateNeightbors(cell2);
            CreateNeightbors(cell3);
            CreateNeightbors(cell4);
        }

        CellInfo TryCreateNeightbor(int x, int y, CellInfo parentCell)
        {

            if (x < 0 || x > _grid.GetUpperBound(0)) return null;
            if (y < 0 || y > _grid.GetUpperBound(1)) return null;
            if (_grid[x, y] != null) return null;
            if (_rng.Next(0, 99) < 50) return null;


            if (_neightborsCount > 999)
            {
                Debug.Log("abort!");
                return null;
            }
            _neightborsCount++;

            var cell = CreateCell(x, y, parentCell);
            parentCell.hasChildren = true;


            if (_rng.Next(0, 99) < CHANCE_CREATE_HALL)
            {
                CreateHall(ref cell);
            }
            else
            {
                CreateHallWay(ref cell);
            }

            return cell;

        }

        CellInfo CreateCell(int x, int y, CellInfo parentCell)
        {
            var cell = new CellInfo();
            cell.gridX = x;
            cell.gridY = y;
            cell.parent = parentCell;
            _grid[x, y] = cell;
            return cell;
        }

        private void CreateHallWay(ref CellInfo cell)
        {
            int middleX = FindMiddleLine(cell.gridX);
            int middleY = FindMiddleLine(cell.gridY);


            cell.x1 = middleX - _rng.Next(1, 3);
            cell.x2 = middleX + _rng.Next(1, 3);
            cell.y1 = middleY - _rng.Next(1, 3);
            cell.y2 = middleY + _rng.Next(1, 3);
            cell.isHall = false;

        }

        private void CreateHall(ref CellInfo cell)
        {
            int middleX = FindMiddleLine(cell.gridX);
            int middleY = FindMiddleLine(cell.gridY);

            cell.x1 = middleX - 5;
            cell.x2 = middleX + 5;
            cell.y1 = middleY - 5;
            cell.y2 = middleY + 5;
            cell.isHall = true;

        }

        private static int FindMiddleLine(int x)
        {
            return (1 + x) * CELL_SIZE - CELL_SIZE / 2;
        }

        void CreatePolygon(CellInfo cell)
        {
            if (cell is null) return;
            CreatePolygonFromTo(cell.x1, cell.x2, cell.y1, cell.y2);
        }

        void CreatePolygonFromTo(int x1, int x2, int y1, int y2)
        {
            int startX = Mathf.Min(x1, x2);
            int startY = Mathf.Min(y1, y2);
            int maxX = Mathf.Max(x1, x2);
            int maxY = Mathf.Max(y1, y2);

            for (int x = startX; x <= maxX; x++)
            {
                for (int y = startY; y <= maxY; y++)
                {
                    if (x == startX || x == maxX || y == startY || y == maxY)
                    {
                        _intMap[x, y] = 2;
                    }
                    else
                    {
                        _intMap[x, y] = 1;
                    }
                }
            }
        }


        class CellInfo
        {
            public int gridX { get; set; }
            public int gridY { get; set; }
            public int x1 { get; set; }
            public int x2 { get; set; }
            public int y1 { get; set; }
            public int y2 { get; set; }
            public bool isHall;
            public CellInfo parent { get; set; }
            public bool hasChildren { get; set; }
        }
    }
}