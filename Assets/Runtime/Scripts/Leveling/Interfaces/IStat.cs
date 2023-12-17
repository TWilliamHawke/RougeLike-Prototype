using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Stats
{
    public interface IStat<out T>
    {
        public abstract T SelectStorage(IStatContainer controller);
    }
}
