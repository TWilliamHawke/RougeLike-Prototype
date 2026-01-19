using UnityEngine;

namespace Core.Input
{
    public interface IScreenPositionReader
    {
        Vector2 ReadScreenPosition();
    }
}