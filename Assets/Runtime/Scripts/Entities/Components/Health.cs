using System.Collections;
using System.Collections.Generic;
using Entities.Behavior;
using Entities.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class Health : MonoBehaviour, IHealthComponent, IEntityComponent
    {
        public event UnityAction OnHealthChange;

        [SerializeField] Body _body;
        [SerializeField] Injector _healthbarCanvasInjector;

        IHaveHealthData _healthData;

        int _currentHealth;

        public int currentHealth => _currentHealth;
        public int maxHealth => _healthData?.maxHealth ?? 100;
        public Vector3 bodyPosition => _body.transform.position;

        void Awake()
        {
            _healthData = GetComponent<IHaveHealthData>();
        }

        public void Init(IHaveHealthData entitiy)
        {
            _currentHealth = _healthData.maxHealth;
            OnHealthChange?.Invoke();
        }

        public void FillToMax()
        {
            _currentHealth = maxHealth;
            OnHealthChange?.Invoke();
        }

        public void RestoreHealth(int health)
        {
            ChangeHealth(health);
        }

        public void DamageHealth(int health)
        {
            ChangeHealth(-health);
        }

        void ChangeHealth(int health)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + health, 0, maxHealth);

            OnHealthChange?.Invoke();

            if (_currentHealth == 0 && _healthData.deathSounds.Length > 0)
            {
                var sound = _healthData.deathSounds.GetRandom();
                _body.PlaySound(sound);
            }
        }
    }
}