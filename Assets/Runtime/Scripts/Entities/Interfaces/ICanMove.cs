using Map;
using UnityEngine;

public interface ICanMove
{
    void ChangeNode(TileNode node);
    Transform transform { get; }
    TileNode currentNode { get; }
}
