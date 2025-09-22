using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Core.UI
{
    public interface IContextActionButton : IPointerClickHandler
    {
        void BindAction(IContextAction action);
        void ClearAction();
        event UnityAction<IContextAction> OnButtonActivation;
    }
}


