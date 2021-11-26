using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace Entities.UI
{
    public class Healthbar : MonoBehaviour
    {
        IHealthComponent _entity;

        [SerializeField] GameObjects _gameObjects;
        [SerializeField] Image _fillImage;
        [SerializeField] Vector3 _shift;
        [Range(0, 0.1f)]
        [SerializeField] float _minVisibleHealth = 0.05f;

        public void SetHealth(IHealthComponent entity)
        {
            _entity = entity;
            _entity.OnHealthChange += UpdateHealthBar;
        }

        public void SetColor(Color color)
        {
            _fillImage.color = color;
        }

        

        void UpdateHealthBar()
        {
            float healthPct = (float)_entity.currentHealth / _entity.maxHealth;
            _fillImage.fillAmount = Mathf.Clamp(healthPct, _minVisibleHealth, 1);
        }

        void LateUpdate()
        {
            var entityPos = _gameObjects.mainCamera.c.WorldToScreenPoint(_entity.transform.position + _shift);
            transform.position = entityPos;
        }
    }
}