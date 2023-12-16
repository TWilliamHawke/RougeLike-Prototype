using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Stats
{
    [CreateAssetMenu(fileName ="Stat", menuName ="Entities/Stat")]
    public class StaticStat : Stat
    {
        [SerializeField] bool _applyMultFirst = true;
        [SerializeField] int _capMin = 0;
        [SerializeField] int _capMax = System.Int32.MaxValue;
        [SerializeField] int _defaultValue = 100;

        public int defaultValue => _defaultValue;
        public int capMax => _capMax;
        public int capMin => _capMin;

        public int ApplyStatMods(int baseValue, int pctBonus, int flatBonus)
        {
            if (_applyMultFirst)
            {
                return Mathf.FloorToInt(baseValue * (1 + pctBonus / 100f)) + flatBonus;
            }

            return Mathf.FloorToInt((baseValue + flatBonus) * (1 + pctBonus / 100f));
        }

        public override IStatStorage CreateStorage(IStatController controller, int startValue)
        {
            var storage = new StaticStatStorage(controller.effectStorage, this);
            storage.SetStatValue(startValue);
            return storage;
        }

        public override IStatStorage CreateStorage(IStatController controller)
        {
            return new StaticStatStorage(controller.effectStorage, this);
        }


    }
}
