using Map.Objects;

namespace Map
{
    public interface IMapActionLogic
    {
        bool isEnable { get; set; }
        void AddActionDependencies(IActionDependenciesProvider provider);
        void DoAction();
        IActionData template { get; }
    }

}

