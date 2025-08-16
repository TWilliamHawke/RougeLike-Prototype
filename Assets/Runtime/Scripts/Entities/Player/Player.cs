using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using Entities.Combat;
using UnityEngine.Events;
using Entities.Stats;
using Abilities;

namespace Entities.PlayerScripts
{
    [RequireComponent(typeof(VisibilityController))]
    [RequireComponent(typeof(PositionController))]
    [RequireComponent(typeof(AudioEffectsController))]
    [RequireComponent(typeof(FactionHandler))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(StatsContainer))]
    public class Player : MonoBehaviour, IAttackTarget, ICanAttack, IAbilityTarget, IObstacleEntity, IEntityWithComponents
    {
        [SerializeField] CustomEvent _onPlayerTurnEnd;

        [SerializeField] PlayerStats _stats;
        [SerializeField] Body _body;
        [SerializeField] ResistSet _testResists;

        AudioClip[] _deathSounds = new AudioClip[0];

        MeleeAttackController _meleeAttackController;
        Health _health;

        public Dictionary<DamageType, int> resists => _testResists.set;
        public IDamageSource damageSource => _stats.CalculateDamageData();
        public Body body => _body;

        public AudioClip[] deathSounds => _deathSounds;
        public Vector3 position => transform.position;

        public event UnityAction<IStatsController> OnStatsInit;

        private void Awake()
        {
            InitComponents();
        }

        void Start()
        {
            _stats.Init(this);
            OnStatsInit?.Invoke(_stats);      
        }

        //used in editor
        public void StartTurn()
        {

        }

        public void Attack(IAttackTarget target)
        {
            _meleeAttackController.StartAttack(target);
        }

        public void SpawnAt(TileNode node)
        {
            MoveTo(node.intPosition);
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
            _meleeAttackController.OnAttackEnd += EndTurn;
            _meleeAttackController.Init(this);

            _health = GetComponent<Health>();

            GetComponent<VisibilityController>().ChangeViewingRange();
        }

        public void EndTurn()
        {
            _onPlayerTurnEnd.Invoke();
        }

        public U GetEntityComponent<U>() where U : IEntityComponent
        {
            return GetComponent<U>();
        }

        public void MoveTo(Vector3 position)
        {
            transform.position = position;
        }
    }
}