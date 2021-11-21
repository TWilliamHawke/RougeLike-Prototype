using System.Collections;
using System.Collections.Generic;
using Core;
using Map.Generator;
using UnityEngine;

namespace Map
{
    public class TilemapController : ScriptableObject
    {
        [SerializeField] GameObjects _gameObjects;

        TileGrid _grid;

        public void CreateGrid(GeneratorConfig config, int[,] intMap)
        {
            _grid = new TileGrid(config, intMap);
        }

        public bool TryGetNode(int x, int y, out TileNode node)
        {
            return _grid.TryGetNodeAt(x, y, out node);
        }

        public bool TryGetNode(Vector3Int pos, out TileNode node)
        {
            return _grid.TryGetNodeAt(pos.x, pos.y, out node);
        }

        public List<TileNode> GetNeighbors(TileNode node)
        {
            return _grid.GetNeighbors(node);
        }

    }
}