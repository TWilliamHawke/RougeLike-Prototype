using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class Health : MonoBehaviour, IHealthComponent
    {
        public static event UnityAction<IHealthbarData> OnHealthInit;
        public static event UnityAction<IHealthbarData> OnEntitDisbled;

        public event UnityAction OnHealthChange;

        [SerializeField] Body _body;
        [SerializeField] Color _healthbarColor = Color.red;

        //hack it should go from unit template
        IHaveInjureSounds _template;
        bool _turnOffSounds = false;

        int _baseHealth = 100;
        int _currentHealth;

        public int currentHealth => _currentHealth;
        public int maxHealth => _baseHealth;
        public Color healthbarColor => _healthbarColor;

        public void Init(IHaveInjureSounds template)
        {
            _template = template;
            Init();
        }

        public void InitWithoutSound()
        {
            _turnOffSounds = true;
            Init();
        }



        void OnDisable()
        {
            OnEntitDisbled?.Invoke(_body);
        }

        public void RestoreHealth(int health)
        {
            ChangeHealth(health);
        }

        public void DamageHealth(int health)
        {
            ChangeHealth(-health);
        }

        void Init()
        {
            _body.Init(this);
            _currentHealth = _baseHealth;
            OnHealthInit?.Invoke(_body);
            OnHealthChange?.Invoke();
        }

        void ChangeHealth(int health)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + health, 0, maxHealth);
            OnHealthChange?.Invoke();

            if(_currentHealth == 0 && !_turnOffSounds)
            {
                var sound = _template.deathSounds.GetRandom();
                _body.PlaySound(sound);
            }
        }
    }
}