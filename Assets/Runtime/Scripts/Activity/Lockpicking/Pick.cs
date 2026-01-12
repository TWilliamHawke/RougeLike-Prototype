using UnityEngine;

namespace Lockpicking
{
    public class Pick : MonoBehaviour
    {
        RectTransform _transform;
        float _posY;

        public void SyncWithSlider(CustomSlider slider)
        {
            _transform = transform as RectTransform;
            _posY = _transform.anchoredPosition.y;

            slider.OnPositionChange += UpdatePosition;
        }

        private void UpdatePosition(float posX)
        {
            _transform.anchoredPosition = new Vector2(posX, _posY);
        }
    }
}
