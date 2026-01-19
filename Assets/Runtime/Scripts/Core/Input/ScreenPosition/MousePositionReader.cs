using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Input
{
    public class MousePositionReader : IScreenPositionReader
    {
        public Vector2 ReadScreenPosition()
        {
            return Mouse.current.position.ReadValue();
        }
    }
}