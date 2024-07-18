using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Stats
{
    [CreateAssetMenu(fileName ="Stat", menuName ="Entities/Stat")]
    public class StaticStat : DisplayedObject, IStat<StaticStatStorage>
    {
        [SerializeField] bool _applyMultFirst = true;
        [SerializeField] int _capMin = 0;
        [SerializeField] int _capMax = System.Int32.MaxValue;
        [SerializeField] int _defaultValue = 0;
        [Range(0, 1f)]
        [SerializeField] float _minReductionMod = .2f;

        public int defaultValue => _defaultValue;
        public int capMax => _capMax;
        public int capMin => _capMin;
        public bool applyMultFirst => _applyMultFirst;
        public float minReductionMod => _minReductionMod;


        public StaticStatStorage CreateStorage(IStatContainer controller)
        {
            var storage = new StaticStatStorage(this);
            return storage;
        }
    }
}
