using System.Collections;
using System.Collections.Generic;
using Entities.AI;
using Entities.Combat;
using Entities.PlayerScripts;
using Map;
using UnityEngine;
using UnityEngine.Events;
using Items;
using Entities.Stats;
using Abilities;

namespace Entities
{
    [RequireComponent(typeof(StateMachine))]
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(FactionHandler))]
    [RequireComponent(typeof(StatsStorage))]
    [RequireComponent(typeof(PositionController))]
    [RequireComponent(typeof(AudioEffectsController))]
    public abstract class Entity : MonoBehaviour,
        IInteractive, IAbilityTarget, IEntityWithAI, IObstacleEntity, IEntityWithComponents, IEntityWithTemplate
    {
        [SerializeField] Body _body;
        [SerializeField] StatList _statList;

        public event UnityAction<Entity> OnDeath;
        public event UnityAction<IStatsController> OnStatsInit;
        public abstract event UnityAction<ITemplateWithBaseStats> OnTemplateApplied;

        public StateMachine stateMachine => GetComponent<StateMachine>();
        public int expForKill => template.expForKill;

        public abstract AudioClip[] deathSounds { get; }
        public abstract void AddLootTo(IItemStorage storage);
        public abstract void RemoveLootFrom(IItemStorage storage);
        public abstract void InitInteractiveZone(IMapZone mapZoneLogic);

        public Body body => _body;
        public abstract ITemplateWithBaseStats template { get; }
        public Vector3 position => transform.position;

        protected void ApplyStartStats(ITemplateWithBaseStats template)
        {
            var statsStorage = GetComponent<StatsStorage>();
            template.InitStats(statsStorage);
            _body.UpdateSkin(template.bodyChar, template.bodyColor);

            var healthStorage = statsStorage.FindContainer(_statList.health);
            healthStorage.OnReachMin += ProceedDeath;
            OnStatsInit?.Invoke(statsStorage);
        }

        private void ProceedDeath()
        {
            var sound = deathSounds.GetRandom();
            _body.PlaySound(sound);
            _body.StartDeathAnimation();
            OnDeath?.Invoke(this);
            OnDeath = null;
        }

        public abstract void Interact(Player player);

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