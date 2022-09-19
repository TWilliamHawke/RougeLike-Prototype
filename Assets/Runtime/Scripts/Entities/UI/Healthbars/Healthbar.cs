using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Entities.UI
{
    public class Healthbar : MonoBehaviour
    {
        IHealthbarData _healthbarData;

        [SerializeField] GameObjects _gameObjects;
        [SerializeField] Image _fillImage;
        [SerializeField] Vector3 _shift;
        [Range(0, 0.1f)]
        [SerializeField] float _minVisibleHealth = 0.05f;

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
            if (_healthbarData is null)
            {
                DestroyImmediate(this);
            }
            else
            {
                var entityPos = _gameObjects.mainCamera.WorldToScreenPoint(_healthbarData.transform.position + _shift);
                transform.position = entityPos;
            }
        }
    }
}