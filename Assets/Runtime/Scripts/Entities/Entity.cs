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

namespace Entities
{
    [RequireComponent(typeof(StateMachine))]
    [RequireComponent(typeof(Health))]
    public abstract class Entity : MonoBehaviour, ICanAttack, IRangeAttackTarget, IAttackTarget,
        IInteractive, IEffectTarget, IEntityWithAI, IHaveLoot, IHaveHealthData, IMortal, IObstacleEntity,
        IFactionMember
    {
        [SerializeField] Body _body;

        protected Body body => _body;
        protected abstract ITemplateWithBaseStats template { get; }

        Health _health;
        EffectStorage _effectStorage;
		public Faction faction { get; private set; }

        public event UnityAction<Entity> OnDeath;
        public event UnityAction<Faction> OnFactionChange;


        public abstract Dictionary<DamageType, int> resists { get; }
        public StateMachine stateMachine => GetComponent<StateMachine>();
        public EffectStorage effectStorage => _effectStorage;
        public int expForKill => template.expForKill;

        public abstract LootTable lootTable { get; }

        public abstract AudioClip[] deathSounds { get; }
        public int maxHealth => template?.health ?? 100;
        public BehaviorType antiPlayerBehavior => faction?.GetAntiPlayerBehavior() ?? BehaviorType.neutral;

        public abstract IDamageSource damageSource { get; }

        public void ReplaceFaction(Faction newFaction)
        {
            faction = newFaction;
            OnFactionChange?.Invoke(newFaction);
        }

        void IAttackTarget.TakeDamage(int damage)
        {
            _health.DamageHealth(damage);
        }

        protected void ApplyStartStats(ITemplateWithBaseStats template)
        {
            _body.UpdateSkin(template.bodyChar, template.bodyColor);
            faction = template.faction;
            _health = GetComponent<Health>();
            _health.Init(this);
            _health.OnHealthChange += CheckHealth;
        }

        protected void InitComponents()
        {
            var meleeAttackController = GetComponent<MeleeAttackController>();
            var movementController = GetComponent<MovementController>();
            movementController.Init(new TileNode(0, 1, true));
            meleeAttackController.Init(this);
            _effectStorage = new EffectStorage(this);
        }

        private void CheckHealth()
        {
            if (_health.currentHealth > 0) return;
            _body.StartDeathAnimation();
            OnDeath?.Invoke(this);
            OnDeath = null;
        }

        public abstract void PlayAttackSound();
        public abstract void Interact(Player player);
    }

    public interface IMortal
    {
        public event UnityAction<Entity> OnDeath;
        int expForKill { get; }
    }
}