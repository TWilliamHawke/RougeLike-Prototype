using Core.UI;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(ComponentInjector))]
    public class ContextActionsHandler : MonoBehaviour, IObserver<IContextActionButton>
    {
        [InjectField] IContextMenu _contextMenu;

        public void StartObserving()
        {
            _contextMenu.AddButtonsObserver(this);
        }

        private void HandleAction(IContextAction contextAction)
        {
            _contextMenu.CloseMenu();
            contextAction.DoAction();
        }

        public void AddToObserve(IContextActionButton target)
        {
            target.OnButtonActivation += HandleAction;
        }

        public void RemoveFromObserve(IContextActionButton target)
        {
            target.OnButtonActivation -= HandleAction;
        }
    }
}
