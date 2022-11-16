using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

namespace Entities.Combat
{
    public class AOEEffect : MonoBehaviour
    {
        [SerializeField] TMP_Text[] _AOE_parts;
        [Range(0, 2)]
        [SerializeField] float _expansionSpeed = 1;
        [Range(0, 2)]
        [SerializeField] float _scaleSpeed = 1;

		public event UnityAction OnAnimationEnd;

        ProjectileTemplate _template;
        float totalMovement = 0;
        bool isFinished = false;
        Vector3[] _defaultPositions;

        const float BASE_SCALE = .25f;
		float maxRadius => _template?.radius ?? 1f;

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
            if (isFinished) return;

            float nextFrameMovement = Time.deltaTime * _expansionSpeed;
            totalMovement += nextFrameMovement;
            var movementVector = Vector3.right * nextFrameMovement;

            foreach (TMP_Text part in _AOE_parts)
            {
                part.transform.Translate(movementVector);
                part.transform.localScale = Vector3.one * BASE_SCALE * (1 + totalMovement * _scaleSpeed);
            }

            if (totalMovement >= maxRadius)
            {
                isFinished = true;
				OnAnimationEnd?.Invoke();
            }
        }

        public void Reset()
        {
            isFinished = false;
			gameObject.SetActive(true);
            for (int i = 0; i < _AOE_parts.Length; i++)
            {
                TMP_Text part = _AOE_parts[i];
                part.transform.localScale = Vector3.one * BASE_SCALE;
                part.transform.localPosition = _defaultPositions[i];
            }
            totalMovement = 0;
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


