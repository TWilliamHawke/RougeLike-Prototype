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
    public class PlayerStats : ScriptableObject, IManaComponent, IHealthbarData, IStatsController
    {
        [SerializeField] AudioClip[] _weaponSounds;
        [SerializeField] StatList _statList;
        [SerializeField] StatValues _defaultStats;
        [SerializeField] CustomEvent _onPlayerStatsInit;

        CappedStatStorage _mana;
        Body _body;

        //public event UnityAction OnHealthChange;

        Dictionary<DamageType, int> _resists = new Dictionary<DamageType, int>(5);
        StatsContainer _statsContainer;

        public AudioClip[] attackSounds => _weaponSounds;
        public int maxMana => _mana.currentValue;
        public int curentMana => _mana.maxValue;
        public EffectStorage effectStorage => _statsContainer.effectStorage;

        public Vector3 bodyPosition => _body.transform.position;

        public BehaviorType behavior => BehaviorType.none;

        public void Init(Player player)
        {
            _statsContainer = new(player);
            _defaultStats.InitStats(_statsContainer);
            _mana = _statsContainer.FindStorage(_statList.mana);
            _body = player.body;
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

        public void AddObserver<T, U>(IObserver<T> observer, IStat<U> stat) where U : T
        {
            _statsContainer.AddObserver(observer, stat);
        }

        public T FindStorage<T>(IStat<T> stat)
        {
            return _statsContainer.FindStorage(stat);
        }
    }
}