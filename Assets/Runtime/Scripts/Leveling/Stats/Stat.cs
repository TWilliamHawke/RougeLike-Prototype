using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Entities.Stats
{
    public class Stat : DisplayedObject
    {
        public virtual IStatStorage CreateStorage(IStatController controller)
        {
            throw new System.Exception("no inplemented");
        }

        public virtual IStatStorage CreateStorage(IStatController controller, int startValue)
        {
            throw new System.Exception("no inplemented");
        }

    }
}
