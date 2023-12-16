using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using Entities.Behavior;
using Entities.Combat;
using UnityEngine.Events;
using Effects;
using Entities.Stats;

namespace Entities.PlayerScripts
{
    [RequireComponent(typeof(VisibilityController))]
    [RequireComponent(typeof(ProjectileController))]
    public class Player : MonoBehaviour, IAttackTarget, ICanAttack, IEffectTarget, IObstacleEntity,
        IHaveHealthData, IFactionMember
    {
        [SerializeField] CustomEvent _onPlayerTurnEnd;

        [SerializeField] PlayerStats _stats;
        [SerializeField] Body _body;
        [SerializeField] ResistSet _testResists;
        [SerializeField] ActiveAbilities _activeAbilities;
        [SerializeField] Faction _playerFaction;
        [SerializeField] Stat _healthStat;

        AudioClip[] _deathSounds = new AudioClip[0];

        MovementController _movementController;
        MeleeAttackController _meleeAttackController;
        Health _health;
        IInteractive _target;
        StatsController _statsController;

        public Dictionary<DamageType, int> resists => _testResists.set;
        public IDamageSource damageSource => _stats.CalculateDamageData();
        public IAudioSource body => _body;

        public EffectStorage effectStorage => _statsController.effectStorage;
        public AudioClip[] deathSounds => _deathSounds;
        public int maxHealth => 100;

        public Faction faction => _playerFaction;

        public event UnityAction<Faction> OnFactionChange;

        private void Awake()
        {
            _statsController = new(this);
            _statsController.InitStat(_healthStat, 100);
            InitComponents();
            _stats.SubscribeOnHealthEvents(this);
            _activeAbilities.SetController(GetComponent<AbilityController>());
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
            _health.Init(this);

            GetComponent<VisibilityController>().ChangeViewingRange();
            GetComponent<ProjectileController>().OnAttackEnd += EndPlayerTurn;
            GetComponent<AbilityController>().Init();
        }

        void EndPlayerTurn()
        {
            _onPlayerTurnEnd.Invoke();
        }

        void IFactionMember.ReplaceFaction(Faction newFaction)
        {
            _playerFaction = newFaction;
            OnFactionChange?.Invoke(newFaction);
        }
    }
}