using UnityEngine;

namespace Core.Input
{
    public class ScreenPositionReader : MonoBehaviour, IScreenPositionReader
    {
        [InjectField] InputController _inputController;

        IScreenPositionReader _selectedReader;
        IScreenPositionReader _mouseReader;
        IScreenPositionReader _touchReader;

        public void CreateReaders()
        {
            _mouseReader = new MousePositionReader();
            _touchReader = new TouchPositionReader(_inputController);
            _selectedReader = _mouseReader;
        }

        public Vector2 ReadScreenPosition()
        {
            return _selectedReader.ReadScreenPosition();
        }

        public void SwitchToMouseReader()
        {
            _selectedReader = _mouseReader;
        }

        public void SwitchToTouchReader()
        {
            _selectedReader = _touchReader;
        }
    }
}