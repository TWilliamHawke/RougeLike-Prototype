using UnityEngine;

namespace Map
{
    public interface IMapAction : IContextAction
    {
        bool isEnable { get; }
        Sprite icon { get; }
    }

}

