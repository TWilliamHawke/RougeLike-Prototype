using System.Collections;
using System.Collections.Generic;
using Entities.Behavior;
using Entities.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class Health : MonoBehaviour, IHealthComponent, IHealthbarData, IInjectionTarget
    {
        public event UnityAction OnHealthChange;

        [SerializeField] Body _body;
        [SerializeField] Injector _healthbarCanvasInjector;

        [InjectField] HealthbarCanvas _healthbarCanvas;

        IHaveHealthData _healthData;

        int _currentHealth;

        public int currentHealth => _currentHealth;
        public int maxHealth => _healthData.maxHealth;
        public Vector3 bodyPosition => _body.transform.position;

        public bool waitForAllDependencies => false;

        public BehaviorType behavior => _healthData.antiPlayerBehavior;

        void Awake()
        {
            _healthData = GetComponent<IHaveHealthData>();
            _currentHealth = _healthData.maxHealth;
            _healthbarCanvasInjector.AddInjectionTarget(this);
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

        void IInjectionTarget.FinalizeInjection()
        {
            _healthbarCanvas.CreateNewHealthbar(this);
            OnHealthChange?.Invoke();
        }
    }
}