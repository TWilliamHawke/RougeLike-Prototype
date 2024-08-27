using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using Entities.Behavior;
using Entities.Combat;
using UnityEngine.Events;
using Effects;
using Entities.Stats;
using Abilities;

namespace Entities.PlayerScripts
{
    [RequireComponent(typeof(VisibilityController))]
    [RequireComponent(typeof(ProjectileController))]
    [RequireComponent(typeof(FactionHandler))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(StatsContainer))]
    public class Player : MonoBehaviour, IAttackTarget, ICanAttack, IAbilityTarget, IObstacleEntity, IEntityWithComponents
    {
        [SerializeField] CustomEvent _onPlayerTurnEnd;

        [SerializeField] PlayerStats _stats;
        [SerializeField] Body _body;
        [SerializeField] ResistSet _testResists;
        [SerializeField] ActiveAbilities _activeAbilities;

        AudioClip[] _deathSounds = new AudioClip[0];

        MovementController _movementController;
        MeleeAttackController _meleeAttackController;
        Health _health;
        IInteractive _target;

        public Dictionary<DamageType, int> resists => _testResists.set;
        public IDamageSource damageSource => _stats.CalculateDamageData();
        public Body body => _body;

        public AudioClip[] deathSounds => _deathSounds;

        public event UnityAction<IStatsController> OnStatsInit;

        private void Awake()
        {
            InitComponents();
            _activeAbilities.SetController(GetComponent<AbilityController>());
        }

        void Start()
        {
            _stats.Init(this);
            OnStatsInit?.Invoke(_stats);      
        }

        //used in editor
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
            Vector3 position = node.position;
            transform.position = position;
            _movementController.Init(node);
        }

        public void PlayAttackSound()
        {
            _body.PlaySound(_stats.attackSounds.GetRandom());
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

            GetComponent<VisibilityController>().ChangeViewingRange();
            GetComponent<ProjectileController>().OnAttackEnd += EndPlayerTurn;
            GetComponent<AbilityController>().Init();
        }

        void EndPlayerTurn()
        {
            _onPlayerTurnEnd.Invoke();
        }

        public U GetEntityComponent<U>() where U : IEntityComponent
        {
            return GetComponent<U>();
        }
    }
}