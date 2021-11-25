using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Entities
{
    public class Health : MonoBehaviour, IHaveHealth
    {
        public static event UnityAction<Health> OnEntityEnabled;
        public static event UnityAction<Health> OnEntitDisbled;

        public event UnityAction<int> OnHealthChange;

        int _baseHealth = 100;
        int _currentHealth;

        public int currentHealth => _currentHealth;
        public int maxHealth => _baseHealth;

        public void Init()
        {
			Debug.Log("Enabled");
            _currentHealth = _baseHealth;
            OnEntityEnabled?.Invoke(this);
        }

        void OnDisable()
        {
			OnEntitDisbled?.Invoke(this);
        }

		public void ChangeHealth(int health)
		{
            _currentHealth = health;
			OnHealthChange?.Invoke(_currentHealth);
		}
    }
}