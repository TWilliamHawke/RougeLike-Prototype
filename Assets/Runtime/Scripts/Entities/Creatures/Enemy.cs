using System.Collections;
using System.Collections.Generic;
using Entities.AI;
using Entities.Behavior;
using Entities.Combat;
using Entities.PlayerScripts;
using Map;
using UnityEngine;
using TMPro;
using Effects;
using UnityEngine.Events;
using Items;

namespace Entities
{
    [RequireComponent(typeof(StateMachine))]
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour, ICanAttack, IRangeAttackTarget, IAttackTarget, 
        IInteractive, IEffectTarget, IEntityWithAI, IHaveLoot, IObstacleEntity
    {
        [SerializeField] SpriteRenderer _spriteRanderer;
        [SerializeField] EnemyTemplate _template;
        [SerializeField] Body _body;

        Health _health;
        EffectStorage _effectStorage;

        public event UnityAction<Enemy> OnDeath;

        public IDamageSource damageSource => _template;
        public Dictionary<DamageType, int> resists => _template.resists.set;
        public StateMachine stateMachine => GetComponent<StateMachine>();
        public TileNode currentNode => throw new System.NotImplementedException();
        public EffectStorage effectStorage => _effectStorage;
        public int expForKill => _template.expForKill;

        public LootTable lootTable => _template.lootTable;

        public void Init(EnemyTemplate template)
        {
            _template = template;
            Init();
        }

        public void Init()
        {
            InitComponents();
            ApplyStartStats();
            _effectStorage = new EffectStorage(this);
        }

        public void ApplyStartStats()
        {
            _body.UpdateSkin(_template.bodyChar, _template.bodyColor);
        }

        public void Interact(Player player)
        {
            player.Attack(this);
        }

        public void TakeDamage(int damage)
        {
            _health.DamageHealth(damage);
        }

        public void PlayAttackSound()
        {
            _body.PlaySound(_template.attackSounds.GetRandom());
        }

        private void InitComponents()
        {
            _health = GetComponent<Health>();
            _health.Init(_template.sounds);
            _health.OnHealthChange += CheckHealth;
            var meleeAttackController = GetComponent<MeleeAttackController>();
            var movementController = GetComponent<MovementController>();
            movementController.Init(new TileNode(0, 1, true));
            meleeAttackController.Init(this);
        }

        private void CheckHealth()
        {
            if(_health.currentHealth > 0) return;
            OnDeath?.Invoke(this);
            OnDeath = null;
        }

    }
}