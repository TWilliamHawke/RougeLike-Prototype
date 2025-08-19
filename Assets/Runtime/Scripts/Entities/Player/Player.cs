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
    [RequireComponent(typeof(StatsStorage))]
    public class Player : MonoBehaviour, IAbilityTarget, IObstacleEntity, IEntityWithComponents
    {
        [SerializeField] CustomEvent _onPlayerTurnEnd;

        [SerializeField] PlayerStats _stats;
        [SerializeField] QuickBarDataStorage _quickBarData;

        AudioClip[] _deathSounds = new AudioClip[0];

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

        public void UseMainAbility(IAbilityTarget target)
        {
            _quickBarData.mainAbility.UseAbility(target);
        }

        public void SpawnAt(TileNode node)
        {
            transform.position = node.position;
        }

        void InitComponents()
        {
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
    }
}