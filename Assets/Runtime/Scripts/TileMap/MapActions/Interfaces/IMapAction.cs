using UnityEngine;

namespace Map
{
    public interface IMapAction : IContextAction
    {
        bool isEnable { get; }
        bool isHidden { get; }
        Sprite icon { get; }
    }

}

