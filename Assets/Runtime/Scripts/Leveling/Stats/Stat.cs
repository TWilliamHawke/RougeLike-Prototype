using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Stats
{
    public abstract class Stat<T> : DisplayedObject
    {
        public abstract T SelectStorage(IStatContainer controller);
    }
}
