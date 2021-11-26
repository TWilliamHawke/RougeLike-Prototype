using System.Collections;
using System.Collections.Generic;
using Entities.AI;
using Entities.Behavior;
using Entities.Combat;
using Entities.Player;
using Map;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(StateMachine))]
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour, ICanAttack, IAttackTarget, IInteractive, ICanMove
    {
        [SerializeField] SpriteRenderer _spriteRanderer;
        [SerializeField] EnemyTemplate _template;
        [SerializeField] MovementController _movementController;
        [SerializeField] MeleeAttackController _meleeAttackController;

        Health _health;

        int _currentHealth;
        Dictionary<DamageType, int> _currentResists = new Dictionary<DamageType, int>(5);

        public int maxHealth => _template.health;
        public int currentHealth => _currentHealth;
        public IDamageSource damageSource => _template;
        public Dictionary<DamageType, int> resists => _currentResists;
        public StateMachine stateMachine => GetComponent<StateMachine>();

        public TileNode currentNode => throw new System.NotImplementedException();

        public void Init()
        {
            _health = GetComponent<Health>();
            _health.Init();
            _movementController.Init(this);
            _meleeAttackController.Init(this);

            ApplyStartStats();
        }

        public void ApplyStartStats()
        {
            _currentHealth = _template.health;
            _currentResists[DamageType.physical] = 0;
        }

        public void Interact(PlayerCore player)
        {
            player.Attack(this);
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            _health.ChangeHealth(_currentHealth);

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        void Die()
        {
            Debug.Log("Die");
        }

        public void MoveTo(TileNode node)
        {

        }

        public void TeleportTo(Vector3 position)
        {
            transform.position = position;
        }
    }
}