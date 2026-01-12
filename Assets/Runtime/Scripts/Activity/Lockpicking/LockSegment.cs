using UnityEngine;

namespace Lockpicking
{
    public class LockSegment : MonoBehaviour
    {
        [SerializeField] int _pegOpenPosition;
        [SerializeField] float _animationSpeed;
        [SerializeField] AnimationCurve _animationCurve;
        [SerializeField] RectTransform[] _animatedParts;

        float _animationProgress = 0;
        AnimationStates _animationState = AnimationStates.none;

        void Update()
        {
            UpdateAnimation();
        }

        public void MovePegUp()
        {
            _animationState = AnimationStates.up;
        }

        public void MovePegDown()
        {
            _animationState = AnimationStates.down;
        }

        public void ResetAnimation()
        {
            _animationProgress = 0;
            _animationState = AnimationStates.none;
            _animatedParts.ForEach(part => part.anchoredPosition = Vector2.zero);
        }

        public RectTransform GetPosition()
        {
            var rt = transform as RectTransform;
            return rt;
        }

        private void UpdateAnimation()
        {
            if (_animationState == AnimationStates.none) return;
            _animationProgress += (int)_animationState * Time.deltaTime * _animationSpeed;
            Vector2 targetPosition = _animationCurve.Evaluate(_animationProgress) * _pegOpenPosition * Vector2.down;
            _animatedParts.ForEach(x => x.anchoredPosition = targetPosition);

            if (_animationProgress >= 1 || _animationProgress <= 0)
            {
                FinalizeAnimation();
            }
        }

        private void FinalizeAnimation()
        {
            _animationProgress = 0;
            _animationState = AnimationStates.none;
        }

        enum AnimationStates
        {
            down = -1,
            none = 0,
            up = 1
        }
    }
}
