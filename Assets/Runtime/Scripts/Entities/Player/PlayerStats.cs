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
    public class PlayerStats : ScriptableObject, IManaComponent, IStatsController
    {
        [SerializeField] AudioClip[] _weaponSounds;
        [SerializeField] StatList _statList;
        [SerializeField] StatValues _defaultStats;
        [SerializeField] CustomEvent _onPlayerStatsInit;

        ResourceStorage _mana;

        Dictionary<DamageType, int> _resists = new Dictionary<DamageType, int>(5);
        StatsContainer _statsContainer;
        EffectStorage _effectStorage;

        public AudioClip[] attackSounds => _weaponSounds;
        public int maxMana => _mana.currentValue;
        public int curentMana => _mana.maxValue;
        public EffectStorage effectStorage => _effectStorage;


        public void Init(Player player)
        {
            _effectStorage = new(player);
            _statsContainer = player.GetEntityComponent<StatsContainer>();
            _defaultStats.InitStats(_statsContainer);
            _mana = _statsContainer.FindStorage(_statList.mana);
            _onPlayerStatsInit.Invoke();
        }

        public IDamageSource CalculateDamageData()
        {
            //HACK this code should return weapon or skill stats
            return new DamageSource(10, 20, DamageType.physical);
        }

        public Dictionary<DamageType, int> CalculateCurrentResists()
        {
            //apply effects, armour, etc...
            _resists[DamageType.physical] = 10;

            return _resists;
        }

        public bool TrySpendMana(int count)
        {
            return _mana.TryReduceStat(count);
        }

        public void AddObserver(IObserver<StaticStatStorage> observer, StaticStat stat)
        {
            _statsContainer.AddObserver(observer, stat);
        }

        public void AddObserver(IObserver<ResourceStorage> observer, StoredResource stat)
        {
            _statsContainer.AddObserver(observer, stat);
        }

        public StaticStatStorage FindStorage(StaticStat stat)
        {
            return _statsContainer.FindStorage(stat);
        }

        public ResourceStorage FindStorage(StoredResource stat)
        {
            return _statsContainer.FindStorage(stat);
        }
    }
}