using Map.Objects;
using UnityEngine.Events;

namespace Map
{
    public interface IMapActionLogic
    {
        bool isEnable { get; set; }
        void AddActionDependencies(IActionDependenciesProvider provider);
        void DoAction();
        IActionData template { get; }
        event UnityAction<IMapActionLogic> OnCompletion;
    }

}

