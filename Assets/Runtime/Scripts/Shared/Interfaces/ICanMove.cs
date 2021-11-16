using Map;
using UnityEngine;

public interface ICanMove
{
    void MoveTo(TileNode node);
    void TeleportTo(Vector3 position);
    Transform transform { get; }
    TileNode currentNode { get; }
}
