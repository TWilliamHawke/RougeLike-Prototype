using UnityEngine;

namespace Map
{
    public interface IMapNodeStorage
    {
        TileNode GetNode(Vector3Int pos);
        bool TryGetNode(Vector3Int pos, out TileNode node);
        bool TryGetNodeAt(int x, int y, out TileNode node);
    }
}