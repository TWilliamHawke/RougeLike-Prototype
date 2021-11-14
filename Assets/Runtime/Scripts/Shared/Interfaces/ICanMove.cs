using UnityEngine;

internal interface ICanMove
{
    void MoveTo(Vector3 position);
    void TeleportTo(Vector3 position);
}
