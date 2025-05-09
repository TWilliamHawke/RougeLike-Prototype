using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace Entities.Combat
{
    public class AoeAnimation : MonoBehaviour
    {
        [SerializeField] TMP_Text[] _AOE_parts;
        [Range(0, 2)]
        [SerializeField] float _expansionSpeed = 1;
        [Range(0, 2)]
        [SerializeField] float _scaleSpeed = 1;
        [Range(0, 1)]
        [SerializeField] float _damageFrame = .5f;
        [Range(0, .5f)]
        [SerializeField] float _maxRadiusVisualAddition = .2f;

        public event UnityAction OnAnimationEnd;
        public event UnityAction<int> OnDamageFrame;

        public ProjectileTemplate template => _template;
        public Vector3Int tilepos => transform.position.ToTilePos();

        ProjectileTemplate _template;
        float _animationProgress = 0;
        bool _isFinished = false;
        bool _damageFrameIsHappened = false;
        Vector3[] _defaultPositions;


        const float BASE_SCALE = .25f;
        int maxRadius => _template?.radius ?? 1;

        private void Awake()
        {
            _defaultPositions = new Vector3[_AOE_parts.Length];

            for (int i = 0; i < _AOE_parts.Length; i++)
            {
                _defaultPositions[i] = _AOE_parts[i].transform.localPosition;
            }
        }

        private void Update()
        {
            if (_isFinished) return;

            float nextFrameProgress = Time.deltaTime * _expansionSpeed;
            _animationProgress += nextFrameProgress;
            var movementVector = Vector3.right * nextFrameProgress;

            float fractionalPart = _animationProgress - Mathf.Floor(_animationProgress);

            if (fractionalPart >= _damageFrame && !_damageFrameIsHappened)
            {
                int damageRadius = Mathf.FloorToInt(_animationProgress) + 1;
                if (damageRadius <= maxRadius)
                {
                    OnDamageFrame?.Invoke(damageRadius);
                    _damageFrameIsHappened = true;
                }
            }

            if (fractionalPart < _damageFrame && _damageFrameIsHappened)
            {
                _damageFrameIsHappened = false;
            }

            foreach (TMP_Text part in _AOE_parts)
            {
                part.transform.Translate(movementVector);
                part.transform.localScale = Vector3.one * BASE_SCALE * (1 + _animationProgress * _scaleSpeed);
            }

            if (_animationProgress >= maxRadius + _maxRadiusVisualAddition)
            {
                _isFinished = true;
                OnAnimationEnd?.Invoke();
            }
        }

        public void Reset()
        {
            _isFinished = false;
            gameObject.SetActive(true);
            for (int i = 0; i < _AOE_parts.Length; i++)
            {
                TMP_Text part = _AOE_parts[i];
                part.transform.localScale = Vector3.one * BASE_SCALE;
                part.transform.localPosition = _defaultPositions[i];
            }
            _animationProgress = 0;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetTemplate(ProjectileTemplate template)
        {
            _template = template;
            foreach (TMP_Text part in _AOE_parts)
            {
                part.color = template.color;
            }

        }
    }
}


