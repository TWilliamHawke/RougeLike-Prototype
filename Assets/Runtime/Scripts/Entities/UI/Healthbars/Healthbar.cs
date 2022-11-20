using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Entities.UI
{
    public class Healthbar : MonoBehaviour, IInjectionTarget
    {
        IHealthbarData _healthbarData;

        [SerializeField] Injector _mainCameraInjector;
        [SerializeField] Image _fillImage;
        [SerializeField] Vector3 _shift;
        [Range(0, 0.1f)]
        [SerializeField] float _minVisibleHealth = 0.05f;

        [InjectField] Camera _mainCamera;

        public bool waitForAllDependencies => false;

        private void Awake()
        {
            _mainCameraInjector.AddInjectionTarget(this);
        }

        public void SetHealth(IHealthbarData entity)
        {
            _healthbarData = entity;
            _healthbarData.OnHealthChange += UpdateHealthBar;
        }

        public void SetColor(Color color)
        {
            _fillImage.color = color;
        }

        void UpdateHealthBar()
        {
            float healthPct = (float)_healthbarData.currentHealth / _healthbarData.maxHealth;
            _fillImage.fillAmount = Mathf.Clamp(healthPct, _minVisibleHealth, 1);
        }

        void LateUpdate()
        {
            if (_mainCamera is null) return;
            if (_healthbarData is null)
            {
                DestroyImmediate(this);
            }
            else
            {
                var entityPos = _mainCamera.WorldToScreenPoint(_healthbarData.transform.position + _shift);
                transform.position = entityPos;
            }
        }

        public void FinalizeInjection()
        {
        }
    }
}