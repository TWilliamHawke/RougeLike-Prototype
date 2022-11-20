using System.Collections;
using System.Collections.Generic;
using Entities.UI;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class Health : MonoBehaviour, IHealthComponent, IHealthbarData, IInjectionTarget
    {
        public event UnityAction OnHealthChange;

        [SerializeField] Body _body;
        [SerializeField] Color _healthbarColor = Color.red;
        [SerializeField] Injector _healthbarCanvasInjector;

        [InjectField] HealthbarCanvas _healthbarCanvas;

        //hack it should go from unit template
        IHaveInjureSounds _template;
        bool _enableInjureSounds = false;

        int _baseHealth = 25;
        int _currentHealth;

        public int currentHealth => _currentHealth;
        public int maxHealth => _baseHealth;
        public Color healthbarColor => _healthbarColor;
        public Vector3 bodyPosition => _body.transform.position;

        public bool waitForAllDependencies => false;

        void Awake()
        {
            _currentHealth = _baseHealth;
            _healthbarCanvasInjector.AddInjectionTarget(this);
        }

        public void SetInjureSounds(IHaveInjureSounds template)
        {
            _template = template;
            _enableInjureSounds = true;
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

            if (_currentHealth == 0 && _enableInjureSounds)
            {
                var sound = _template.deathSounds.GetRandom();
                _body.PlaySound(sound);
            }
        }

        public void FinalizeInjection()
        {
            _healthbarCanvas.CreateNewHealthbar(this);
            OnHealthChange?.Invoke();
        }
    }
}