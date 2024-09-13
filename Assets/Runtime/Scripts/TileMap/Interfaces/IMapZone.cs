using Map.Zones;
using UnityEngine;

namespace Map
{
    public interface IMapZone : IObserver<InteractionZone>
    {
        string displayName { get; }
        Sprite icon { get; }
        IMapActionList actionList { get; }
        TaskData currentTask { get; }
    }
}

