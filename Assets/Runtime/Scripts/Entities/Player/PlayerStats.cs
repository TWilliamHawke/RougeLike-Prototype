using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using UnityEngine;
using UnityEngine.Events;
using Effects;
using Core;
using Entities.Stats;
using Entities.Behavior;

namespace Entities.PlayerScripts
{
    public class PlayerStats : ScriptableObject, IStatsController
    {
        [SerializeField] AudioClip[] _weaponSounds;
        [SerializeField] StatList _statList;
        [SerializeField] StatValues _defaultStats;
        [SerializeField] CustomEvent _onPlayerStatsInit;

        StatsStorage _statsContainer;

        public AudioClip[] attackSounds => _weaponSounds;


        public void Init(Player player)
        {
            _statsContainer = player.GetEntityComponent<StatsStorage>();
            _defaultStats.InitStats(_statsContainer);
            _onPlayerStatsInit.Invoke();
        }

        public void AddObserver(IObserver<StaticStatStorage> observer, StaticStat stat)
        {
            _statsContainer.AddObserver(observer, stat);
        }

        public void AddObserver(IObserver<ResourceContainer> observer, StoredResource stat)
        {
            _statsContainer.AddObserver(observer, stat);
        }

        public StaticStatStorage FindContainer(StaticStat stat)
        {
            return _statsContainer.FindContainer(stat);
        }

        public ResourceContainer FindContainer(StoredResource stat)
        {
            return _statsContainer.FindContainer(stat);
        }
    }
}