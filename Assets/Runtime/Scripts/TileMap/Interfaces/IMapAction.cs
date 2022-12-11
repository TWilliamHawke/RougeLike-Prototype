using Map.Objects;
using UnityEngine;
using UnityEngine.Events;

namespace Map
{
    public interface IMapAction : IContextAction
    {
        bool isEnable { get; set; }
        Sprite icon { get; }
        event UnityAction<IMapAction> OnCompletion;
    }

}

