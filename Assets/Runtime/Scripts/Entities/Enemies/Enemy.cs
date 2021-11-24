using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using Entities.PlayerScripts;
using UnityEngine;

namespace Entities
{
    public class Enemy : MonoBehaviour, ICanAttack, IAttackTarget, IInteractive
    {
        [SerializeField] SpriteRenderer _spriteRanderer;
        [SerializeField] EnemyTemplate _template;

        int _currentHealth;
        Dictionary<DamageType, int> _currentResists = new Dictionary<DamageType, int>(5);

        public int maxHealth => _template.health;
        public int currentHealth => _currentHealth;
        public IDamageSource damageSource => _template;
        public Dictionary<DamageType, int> resists => _currentResists;

        private void Awake()
        {
            ApplyStartStats();
        }

        public void ApplyStartStats()
        {
            _currentHealth = _template.health;
            _currentResists[DamageType.physical] = 0;
        }

        public void Interact(Player player)
        {
            player.Attack(this);
        }

        public void TakeDamage(int damage)
        {
            Debug.Log($"Damage taken: {damage} hp");
            _currentHealth -= damage;
			
            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            Debug.Log("Die");
        }
    }
}