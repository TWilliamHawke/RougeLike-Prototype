using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Stats
{
    [CreateAssetMenu(fileName = "StatValues", menuName = "Entities/Stat Values")]
    public class StatValues : ScriptableObject
    {
        [HideInInspector]
        [SerializeField] StatList _statList;
        [SerializeField] int _health;
        [SerializeField] int _mana;
        [Space()]
        [SerializeField]
        List<StatValue> _staticStats;

        public void InitStats(IResourceContainer controller)
        {
            controller.InitStat(_statList.health, _health);
            controller.InitStat(_statList.mana, _mana);
            _staticStats.ForEach(s => controller.InitStat(s.stat, s.value));
        }
    }

    [System.Serializable]
    public class StatValue
    {
        public StoredResource stat;
        public int value;
    }
}
