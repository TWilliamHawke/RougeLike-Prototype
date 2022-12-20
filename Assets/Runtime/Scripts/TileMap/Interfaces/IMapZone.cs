using UnityEngine;

namespace Map
{
    public interface IMapZone
    {
        string displayName { get; }
        Sprite icon { get; }
        IMapActionList mapActionList { get; }
        TaskData currentTask { get; }
    }
}

