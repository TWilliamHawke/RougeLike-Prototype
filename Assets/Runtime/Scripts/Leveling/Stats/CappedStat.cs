using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Stats
{
    [CreateAssetMenu(fileName ="Stat", menuName ="Entities/CappedStat")]
    public class CappedStat : Stat
    {
        [SerializeField] StaticStat _parentStat;

        public override IStatStorage CreateStorage(IStatController controller, int startValue)
        {
            var storage = new CappedStatStorage();
            controller.InitStat(_parentStat, startValue);
            controller.AddObserver(storage, _parentStat);
            return storage;
        }

        public override IStatStorage CreateStorage(IStatController controller)
        {
            var storage = new CappedStatStorage();
            controller.AddObserver(storage, _parentStat);
            return storage;
        }
    }
}
