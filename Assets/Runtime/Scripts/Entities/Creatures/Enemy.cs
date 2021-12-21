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
    public class Enemy : MonoBehaviour, ICanAttack, IRangeAttackTarget, IAttackTarget, IInteractive
    {
        [SerializeField] SpriteRenderer _spriteRanderer;
        [SerializeField] EnemyTemplate _template;
        [SerializeField] TMP_Text _TMPSprite;

        Health _health;


        public IDamageSource damageSource => _template;
        public Dictionary<DamageType, int> resists => _template.resists.set;
        public StateMachine stateMachine => GetComponent<StateMachine>();

        public TileNode currentNode => throw new System.NotImplementedException();

        public AudioClip[] attackSounds => _template.attackSounds;

        public void Init()
        {
            InitComponents();
            ApplyStartStats();
        }

        public void ApplyStartStats()
        {
            _TMPSprite.text = _template.bodyChar;
            _TMPSprite.color = _template.bodyColor;
        }

        public void Interact(PlayerCore player)
        {
            player.Attack(this);
        }

        public void TakeDamage(int damage)
        {
            _health.DamageHealth(damage);
        }

        private void InitComponents()
        {
            _health = GetComponent<Health>();
            _health.Init(_template.sounds);
            var meleeAttackController = GetComponent<MeleeAttackController>();
            var movementController = GetComponent<MovementController>();
            movementController.Init(new TileNode(0, 1, true));
            meleeAttackController.Init(this);
        }


    }
}