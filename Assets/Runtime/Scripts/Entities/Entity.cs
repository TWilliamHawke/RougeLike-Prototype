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
    [RequireComponent(typeof(FactionHandler))]
    public abstract class Entity : MonoBehaviour, ICanAttack, IRangeAttackTarget, IAttackTarget,
        IInteractive, IEffectTarget, IEntityWithAI, IHaveLoot, IMortal, IObstacleEntity, IEntityWithComponents, IEntityWithTemplate
    {
        [SerializeField] Body _body;
        [SerializeField] StatList _statList;


        IStatValueController _health;
        EffectStorage _effectStorage;
        StatsContainer _statsContainer;

        public event UnityAction<Entity> OnDeath;
        public event UnityAction<IStatsController> OnStatsInit;
        public abstract event UnityAction<ITemplateWithBaseStats> OnTemplateApplied;

        public StateMachine stateMachine => GetComponent<StateMachine>();
        public EffectStorage effectStorage => _effectStorage;
        public int expForKill => template.expForKill;

        public abstract Dictionary<DamageType, int> resists { get; }
        public abstract LootTable lootTable { get; }
        public abstract AudioClip[] deathSounds { get; }
        public abstract IDamageSource damageSource { get; }

        protected Body body => _body;
        public abstract ITemplateWithBaseStats template { get; }

        void IAttackTarget.TakeDamage(int damage)
        {
            _health.ChangeStat(-damage);
        }

        protected void ApplyStartStats(ITemplateWithBaseStats template)
        {
            _statsContainer = new(this);
            template.InitStats(_statsContainer);
            _body.UpdateSkin(template.bodyChar, template.bodyColor);

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
            var sound = deathSounds.GetRandom();
            _body.PlaySound(sound);
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

        public U GetEntityComponent<U>() where U : IEntityComponent
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