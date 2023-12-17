using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Stats
{
    [CreateAssetMenu(fileName ="Stat", menuName ="Entities/Stat")]
    public class StaticStat : Stat<StaticStatStorage>
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

        public StaticStatStorage CreateStorage(IStatContainer controller)
        {
            var storage = new StaticStatStorage(controller.effectStorage, this);
            controller.staticStatStorage.Add(this, storage);
            return storage;
        }

        public override StaticStatStorage SelectStorage(IStatContainer controller)
        {
            if (!controller.staticStatStorage.TryGetValue(this, out var storage))
            {
                storage = CreateStorage(controller);
                var c = controller as StatsContainer;
                IParentStat s = c.FindStorage<IParentStat, StaticStatStorage>(this);
            }
            return storage;
        }
    }
}
