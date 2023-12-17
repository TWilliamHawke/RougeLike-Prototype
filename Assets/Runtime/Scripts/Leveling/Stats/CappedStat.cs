using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.Stats
{
    [CreateAssetMenu(fileName ="Stat", menuName ="Entities/CappedStat")]
    public class CappedStat : DisplayedObject, IStat<CappedStatStorage>
    {
        [SerializeField] StaticStat _parentStat;

        public CappedStatStorage CreateStorage(IStatContainer controller)
        {
            var storage = new CappedStatStorage();
            controller.AddObserver(storage, _parentStat);
            controller.cappedStatStorage.Add(this, storage);
            return storage;
        }

        public CappedStatStorage SelectStorage(IStatContainer controller)
        {
            if (!controller.cappedStatStorage.TryGetValue(this, out var storage))
            {
                storage = CreateStorage(controller);
            }
            return storage;
        }
    }
}
