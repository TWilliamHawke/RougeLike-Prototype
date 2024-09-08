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
using Abilities;

namespace Entities
{
    [RequireComponent(typeof(StateMachine))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(FactionHandler))]
    [RequireComponent(typeof(StatsContainer))]
    public abstract class Entity : MonoBehaviour, ICanAttack, IRangeAttackTarget, IAttackTarget,
        IInteractive, IAbilityTarget, IEntityWithAI, IHaveLoot, IObstacleEntity, IEntityWithComponents, IEntityWithTemplate
    {
        [SerializeField] Body _body;
        [SerializeField] StatList _statList;


        IStatValueController _health;

        public event UnityAction<Entity> OnDeath;
        public event UnityAction<IStatsController> OnStatsInit;
        public abstract event UnityAction<ITemplateWithBaseStats> OnTemplateApplied;

        public StateMachine stateMachine => GetComponent<StateMachine>();
        public int expForKill => template.expForKill;

        public abstract Dictionary<DamageType, int> resists { get; }
        public abstract LootTable lootTable { get; }
        public abstract AudioClip[] deathSounds { get; }
        public abstract IDamageSource damageSource { get; }
        public abstract void AddLoot(ItemStorage storage);
        public abstract void RemoveLoot(ItemStorage storage);

        public Body body => _body;
        public abstract ITemplateWithBaseStats template { get; }

        void IAttackTarget.TakeDamage(int damage)
        {
            _health.ChangeStat(-damage);
        }

        protected void ApplyStartStats(ITemplateWithBaseStats template)
        {
            var statsContainer = GetEntityComponent<StatsContainer>();
            template.InitStats(statsContainer);
            _body.UpdateSkin(template.bodyChar, template.bodyColor);

            var healthStorage = statsContainer.FindStorage(_statList.health);
            healthStorage.OnReachMin += ProceedDeath;
            _health = healthStorage;
            OnStatsInit?.Invoke(statsContainer);
        }

        protected void InitComponents()
        {
            var meleeAttackController = GetComponent<MeleeAttackController>();
            var movementController = GetComponent<MovementController>();
            movementController.Init(new TileNode(0, 1, true));
            meleeAttackController.Init(this);
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

        public U GetEntityComponent<U>() where U : IEntityComponent
        {
            return GetComponent<U>();
        }
    }
}