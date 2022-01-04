using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using Entities.Behavior;
using Entities.Combat;
using UnityEngine.Events;
using Effects;

namespace Entities.Player
{
    [RequireComponent(typeof(VisibilityController))]
    [RequireComponent(typeof(ProjectileController))]
    public class PlayerCore : MonoBehaviour, IAttackTarget, ICanAttack, IEffectTarget
    {
        public event UnityAction OnPlayerTurnEnd;

        [SerializeField] TilemapController _mapController;
        [SerializeField] PlayerStats _stats;
        [SerializeField] Body _body;
        [SerializeField] ResistSet _testResists;
        [SerializeField] ActiveAbilities _activeAbilities;

        MovementController _movementController;
        MeleeAttackController _meleeAttackController;
        Health _health;
        IInteractive _target;

        public Dictionary<DamageType, int> resists => _testResists.set;
        public IDamageSource damageSource => _stats.CalculateDamageData();
        public IAudioSource body => _body;

        public AudioClip[] attackSounds => _stats.attackSounds;

        public void Init()
        {
            InitComponents();
            _stats.SubscribeOnHealthEvents(_health);
            _activeAbilities.SetController(GetComponent<AbilityController>());
        }


        public void StartTurn()
        {
            if (_target != null && _movementController.pathLength <= 1)
            {
                _target.Interact(this);
                _target = null;
                _movementController.ClearPath();
            }
            else
            {
                _movementController.TakeAStep();
            }
        }

        public void Attack(IAttackTarget target)
        {
            _meleeAttackController.StartAttack(target);
        }

        public void GotoRemoteTarget(IInteractive target)
        {
            _target = target;
            _movementController.SetDestination(target);
            _movementController.TakeAStep();
        }

        public void GotoNode(TileNode node)
        {
            _movementController.SetDestination(node);
            _movementController.TakeAStep();
        }

        public void SpawnAt(TileNode node)
        {
            _movementController = GetComponent<MovementController>();
            Vector3 position = node.position2d.AddZ(0);
            transform.position = position;
            _movementController.Init(node);
        }

        void IAttackTarget.TakeDamage(int damage)
        {
            _health.DamageHealth(damage);
        }

        void InitComponents()
        {
            _meleeAttackController = GetComponent<MeleeAttackController>();
            _meleeAttackController.OnAttackEnd += EndPlayerTurn;
            _meleeAttackController.Init(this);

            _movementController = GetComponent<MovementController>();
            _movementController.OnStepEnd += EndPlayerTurn;

            _health = GetComponent<Health>();
            _health.InitWithoutSound();
            _body.Init(_health);

            GetComponent<VisibilityController>().ChangeViewingRange();
            GetComponent<ProjectileController>().OnAttackEnd += EndPlayerTurn;
            GetComponent<AbilityController>().Init();
        }

        void EndPlayerTurn()
        {
            OnPlayerTurnEnd?.Invoke();
        }

    }
}