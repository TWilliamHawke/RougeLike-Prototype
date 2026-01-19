using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.Stats;

namespace Entities.PlayerScripts
{
    public class PlayerStats : ScriptableObject, IStatStorage
    {
        [SerializeField] AudioClip[] _weaponSounds;
        [SerializeField] StatList _statList;
        [SerializeField] StatValues _defaultStats;
        [SerializeField] CustomEvent _onPlayerStatsInit;

        StatsStorage _statsStorage;

        public AudioClip[] attackSounds => _weaponSounds;


        public void Init(Player player)
        {
            _statsStorage = player.GetEntityComponent<StatsStorage>();
            _defaultStats.InitStats(_statsStorage);
            _onPlayerStatsInit.Invoke();
        }

        public void AddObserver(IObserver<StatContainer> observer, StaticStat stat)
        {
            _statsStorage.AddObserver(observer, stat);
        }

        public void AddObserver(IObserver<ResourceContainer> observer, StoredResource stat)
        {
            _statsStorage.AddObserver(observer, stat);
        }

        public StatContainer FindContainer(StaticStat stat)
        {
            return _statsStorage.FindContainer(stat);
        }

        public ResourceContainer FindContainer(StoredResource stat)
        {
            return _statsStorage.FindContainer(stat);
        }
    }
}