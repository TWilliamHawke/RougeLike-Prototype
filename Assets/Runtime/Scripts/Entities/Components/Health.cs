using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class Health : MonoBehaviour, IHealthComponent, IHealthbarData
    {
        public static event UnityAction<IHealthbarData> OnHealthInit;
        public static event UnityAction<IHealthbarData> OnEntitDisbled;

        public event UnityAction OnHealthChange;

        [SerializeField] Body _body;

        //hack it should go from unit template
        IHaveInjureSounds _template;


        int _baseHealth = 100;
        int _currentHealth;

        public int currentHealth => _currentHealth;
        public int maxHealth => _baseHealth;

        public void Init(IHaveInjureSounds template)
        {
            _template = template;
            _currentHealth = _baseHealth;
            _body.Init(this);
            OnHealthInit?.Invoke(_body);
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

        void ChangeHealth(int health)
        {
            _currentHealth = Mathf.Clamp(_currentHealth + health, 0, maxHealth);
            OnHealthChange?.Invoke();

            if(_currentHealth == 0)
            {
                var sound = _template.deathSounds.GetRandom();
                _body.PlaySound(sound);
            }
        }
    }
}