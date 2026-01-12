using UnityEngine;
using UnityEngine.Events;

namespace Lockpicking
{
    [RequireComponent(typeof(RectTransform))]
    public class LockSegmentsWrapper : MonoBehaviour
    {
        [SerializeField] int _baseSegmentWidth = 120;
        [SerializeField] int _baseBorderWidth = 70;
        [SerializeField] float _pegRelativeSize = .5f;
        [SerializeField] CustomSlider _slider;
        [SerializeField] LockSegment[] _segments;

        float currentSegmentWidth;
        float currentBorderWidth;
        float voidWidth;

        int _activeSegmentsCount = 0;
        int _currentPegIdx = -1;

        public event UnityAction<int> OnPegTouch;

        void Start()
        {
            _slider.OnPositionChange += ObserveSlider;
        }

        public void UpdateSegments(int count)
        {
            _currentPegIdx = -1;
            for (int i = 0; i < _segments.Length; i++)
            {
                var segment = _segments[i];
                segment.ResetAnimation();
                segment.gameObject.SetActive(i < count);
            }

            SetWidth(count);
        }

        public void MovePegUp(int idx)
        {
            if(!_segments.IndexIsInsideBounds(idx)) return;
            _segments[idx].MovePegUp();
        }

        public void MovePegsDown(int exeption)
        {
            for(int i = 0; i < _activeSegmentsCount; i++)
            {
                if(i == exeption) continue;
                _segments[i].MovePegDown();
            }
        }

        private void ObserveSlider(float position)
        {
            if (position < voidWidth + currentBorderWidth) return;
            float rawSegmentIdx = (position - voidWidth - currentBorderWidth)
                / currentSegmentWidth;
            if (rawSegmentIdx > _activeSegmentsCount) return;
            int segmentIdx = Mathf.FloorToInt(rawSegmentIdx);
            float decimalPart = rawSegmentIdx - segmentIdx;
            float distanceFromCentre = Mathf.Abs(.5f - decimalPart);

            if(distanceFromCentre > _pegRelativeSize * .5f)
            {   
                if (_currentPegIdx > -1)
                {
                    _currentPegIdx = -1;
                }
                return;
            }

            if(_currentPegIdx == segmentIdx) return;
            
            _currentPegIdx = segmentIdx;
            OnPegTouch?.Invoke(segmentIdx);
        }

        private void SetWidth(int segmentsCount)
        {
            _activeSegmentsCount = segmentsCount;
            float width = _baseSegmentWidth * segmentsCount + _baseBorderWidth * 2;

            if(width > Screen.width)
            {
                currentSegmentWidth = _baseSegmentWidth * Screen.width / width;
                currentBorderWidth = _baseBorderWidth * Screen.width / width;
                width = Screen.width;
            }
            else
            {
                currentSegmentWidth = _baseSegmentWidth;
                currentBorderWidth = _baseBorderWidth;
            }

            voidWidth = (Screen.width - width) *.5f;
            var rt = transform as RectTransform;
            float heignt = rt.sizeDelta.y;
            rt.sizeDelta = new Vector2(width, heignt);
        }
    }
}
