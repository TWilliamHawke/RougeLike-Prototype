using System.Collections;
using System.Collections.Generic;
using Entities.AI;
using Entities.Behavior;
using Entities.Combat;
using Entities.PlayerScripts;
using Map;
using UnityEngine;
using Effects;
using UnityEngine.Events;
using Items;
using Entities.Stats;

namespace Entities
{
    [RequireComponent(typeof(StateMachine))]
    [RequireComponent(typeof(Health))]
    public abstract class Entity : MonoBehaviour, ICanAttack, IRangeAttackTarget, IAttackTarget,
        IInteractive, IEffectTarget, IEntityWithAI, IHaveLoot, IHaveHealthData, IMortal, IObstacleEntity,
        IFactionMember, IHealthbarData, IEntityWithComponents
    {
        [SerializeField] Body _body;
        [SerializeField] StatList _statList;

        protected Body body => _body;
        protected abstract ITemplateWithBaseStats template { get; }

        IStatValueController _health;
        EffectStorage _effectStorage;
        StatsContainer _statsContainer;
        public Faction faction { get; private set; }

        public event UnityAction<Entity> OnDeath;
        public event UnityAction<Faction> OnFactionChange;
        public event UnityAction<IStatsController> OnStatsInit;

        public abstract Dictionary<DamageType, int> resists { get; }
        public StateMachine stateMachine => GetComponent<StateMachine>();
        public EffectStorage effectStorage => _effectStorage;
        public int expForKill => template.expForKill;

        public abstract LootTable lootTable { get; }

        public abstract AudioClip[] deathSounds { get; }
        public int maxHealth => template?.health ?? 100;
        public BehaviorType antiPlayerBehavior => faction?.GetAntiPlayerBehavior() ?? BehaviorType.neutral;

        public abstract IDamageSource damageSource { get; }

        public Vector3 bodyPosition => _body.transform.position;
        public BehaviorType behavior => antiPlayerBehavior;

        public void ReplaceFaction(Faction newFaction)
        {
            faction = newFaction;
            OnFactionChange?.Invoke(newFaction);
        }

        void IAttackTarget.TakeDamage(int damage)
        {
            _health.ChangeStat(-damage);
        }

        protected void ApplyStartStats(ITemplateWithBaseStats template)
        {
            _statsContainer = new(this);
            template.InitStats(_statsContainer);
            _body.UpdateSkin(template.bodyChar, template.bodyColor);
            faction = template.faction;

            var healthStorage = _statsContainer.FindStorage(_statList.health);
            healthStorage.OnReachMin += ProceedDeath;
            _health = healthStorage;
            OnStatsInit?.Invoke(_statsContainer);
        }

        protected void InitComponents()
        {
            var meleeAttackController = GetComponent<MeleeAttackController>();
            var movementController = GetComponent<MovementController>();
            movementController.Init(new TileNode(0, 1, true));
            meleeAttackController.Init(this);
            _effectStorage = new EffectStorage(this);
        }

        private void ProceedDeath()
        {
            _body.StartDeathAnimation();
            OnDeath?.Invoke(this);
            OnDeath = null;
        }

        public abstract void PlayAttackSound();
        public abstract void Interact(Player player);

        public void AddStatObserver<T, U>(IObserver<T>  observer, IStat<U> stat) where U : T
        {
            _statsContainer.AddObserver(observer, stat);
        }

        public T FindStatStorage<T>(IStat<T> stat)
        {
            return _statsContainer.FindStorage(stat);
        }

        public U GetEntityComponent<U>() where U : MonoBehaviour, IEntityComponent
        {
            return GetComponent<U>();
        }
    }

    public interface IMortal
    {
        public event UnityAction<Entity> OnDeath;
        int expForKill { get; }
    }
}