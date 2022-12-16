using UnityEngine;

namespace Map
{
    public interface IMapObject
    {
        string displayName { get; }
        Sprite icon { get; }
        IMapActionList mapActionList { get; }
        TaskData currentTask { get; }
    }
}

