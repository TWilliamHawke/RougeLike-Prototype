using UnityEngine.Events;

namespace Map
{
    public interface IMapObjectBehavior
    {
        public event UnityAction<TaskData> OnTaskChange;
        TaskData task { get; }
        IMapActionsController actionsController { get; }
    }
}

