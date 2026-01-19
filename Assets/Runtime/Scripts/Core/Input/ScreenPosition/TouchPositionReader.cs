using UnityEngine;

namespace Core.Input
{
    public class TouchPositionReader : IScreenPositionReader
    {
        InputController _inputController;

        public TouchPositionReader(InputController inputController)
        {
            _inputController = inputController;
        }

        public Vector2 ReadScreenPosition()
        {
            return _inputController.main.TouchPosition.ReadValue<Vector2>();
        }
    }
}