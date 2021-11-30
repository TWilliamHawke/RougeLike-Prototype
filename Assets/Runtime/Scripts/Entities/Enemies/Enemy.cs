using System.Collections;
using System.Collections.Generic;
using Entities.AI;
using Entities.Behavior;
using Entities.Combat;
using Entities.Player;
using Map;
using UnityEngine;
using TMPro;

namespace Entities
{
    [RequireComponent(typeof(StateMachine))]
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour, ICanAttack, IAttackTarget, IInteractive, ICanMove
    {
        [SerializeField] SpriteRenderer _spriteRanderer;
        [SerializeField] EnemyTemplate _template;
        [SerializeField] TMP_Text _TMPSprite;

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
            InitComponents();
            ApplyStartStats();
        }

        public void ApplyStartStats()
        {
            _currentHealth = _template.health;
            _currentResists[DamageType.physical] = 0;
            _TMPSprite.text = _template.bodyChar;
            _TMPSprite.color = _template.bodyColor;
        }

        public void Interact(PlayerCore player)
        {
            player.Attack(this);
        }

        public void TakeDamage(int damage)
        {
            _health.DecreaseHealth(damage);
        }

        public void ChangeNode(TileNode node)
        {

        }

        public void TeleportTo(Vector3 position)
        {
            transform.position = position;
        }

        private void InitComponents()
        {
            _health = GetComponent<Health>();
            _health.Init(_template);
            var meleeAttackController = GetComponent<MeleeAttackController>();
            var movementController = GetComponent<MovementController>();
            movementController.Init(this);
            meleeAttackController.Init(this);
        }


    }
}