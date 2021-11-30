using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class Health : MonoBehaviour, IHealthComponent
    {
        public static event UnityAction<Health> OnHealthInit;
        public static event UnityAction<Health> OnEntitDisbled;

        public event UnityAction OnHealthChange;

        [SerializeField] AudioSource _audioSource;

        IHaveInjureSounds _template;


        int _baseHealth = 100;
        int _currentHealth;

        public int currentHealth => _currentHealth;
        public int maxHealth => _baseHealth;

        public void Init(IHaveInjureSounds template)
        {
            _template = template;
            _currentHealth = _baseHealth;
            OnHealthInit?.Invoke(this);
        }



        void OnDisable()
        {
            OnEntitDisbled?.Invoke(this);
        }

        public void IncreaseHealth(int health)
        {
            ChangeHealth(health);
        }

        public void DecreaseHealth(int health)
        {
            ChangeHealth(-health);
        }

        void ChangeHealth(int health)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + health, 0, maxHealth);
            OnHealthChange?.Invoke();

            if(_currentHealth == 0)
            {
                var sound = _template.deathSounds.GetRandom();
                _audioSource.PlayOneShot(sound);
            }
        }
    }
}