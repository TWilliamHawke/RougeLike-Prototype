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
        IInteractive, IEffectTarget, IEntityWithAI, IHaveLoot, IObstacleEntity, IHaveHealthData
    {
        [SerializeField] Body _body;

        protected Body body => _body;
        protected abstract EntityTemplate template { get; }

        Health _health;
        EffectStorage _effectStorage;
		Faction _faction;

        public event UnityAction<Entity> OnDeath;
        public event UnityAction OnFactionChange;

        IDamageSource ICanAttack.damageSource => template;

        public abstract Dictionary<DamageType, int> resists { get; }
        public StateMachine stateMachine => GetComponent<StateMachine>();
        public EffectStorage effectStorage => _effectStorage;
        public int expForKill => template.expForKill;

        public abstract LootTable lootTable { get; }

        public abstract AudioClip[] deathSounds { get; }
        public int maxHealth => template?.health ?? 100;
        public BehaviorType antiPlayerBehavior => _faction?.GetAntiPlayerBehavior() ?? BehaviorType.neutral;

        public void ReplaceFaction(Faction faction)
        {
            _faction = faction;
            _health.behavior = _faction.GetAntiPlayerBehavior(); //ugly HACK
            OnFactionChange?.Invoke();
        }

        void IAttackTarget.TakeDamage(int damage)
        {
            _health.DamageHealth(damage);
        }

        protected void ApplyStartStats(EntityTemplate template)
        {
            _body.UpdateSkin(template.bodyChar, template.bodyColor);
            _faction = template.faction;
            _health = GetComponent<Health>();
            _health.behavior = _faction.GetAntiPlayerBehavior();
            _health.OnHealthChange += CheckHealth;
            _health.FillToMax();
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
            OnDeath?.Invoke(this);
            OnDeath = null;
        }

        public abstract void PlayAttackSound();
        public abstract void Interact(Player player);
    }
}