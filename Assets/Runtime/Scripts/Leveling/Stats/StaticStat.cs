using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

namespace Entities.Stats
{
    [CreateAssetMenu(fileName ="Stat", menuName ="Entities/Stat")]
    public class StaticStat : DisplayedObject, IEffectSignature
    {
        [SerializeField] CustomBonusValuesOrder _bonusesOrder;
        [SerializeField] int _minValue = 0;
        [SerializeField] int _maxValue = int.MaxValue;
        [SerializeField] int _defaultValue = 0;
        [Range(0, 1f)]
        [SerializeField] float _minReductionMod = .2f;

        public int defaultValue => _defaultValue;
        public int maxValue => _maxValue;
        public int minValue => _minValue;
        public float minReductionMod => _minReductionMod;
        public IBonusValuesOrder bonusesOrder => _bonusesOrder;


        public StaticStatStorage CreateStorage(IStatStorage controller)
        {
            var storage = new StaticStatStorage(this);
            return storage;
        }
    }
}
