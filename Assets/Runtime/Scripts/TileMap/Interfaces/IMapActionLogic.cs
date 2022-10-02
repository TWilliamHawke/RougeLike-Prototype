using Map.Objects;
using UnityEngine.Events;

namespace Map
{
    public interface IMapActionLogic: IInjectionTarget
    {
        bool isEnable { get; set; }
        void DoAction();
        IIconData template { get; }
        event UnityAction<IMapActionLogic> OnCompletion;
    }

}

