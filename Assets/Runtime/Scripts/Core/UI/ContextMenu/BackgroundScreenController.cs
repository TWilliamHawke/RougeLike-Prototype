using Core.UI;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(ComponentInjector))]
    public class BackgroundScreenController : MonoBehaviour, IObserver<IContextActionButton>
    {
        [SerializeField] UIScreen _backgroundScreen;

        [InjectField] IContextMenu _contextMenu;

        public void StartObserving()
        {
            _contextMenu.AddButtonsObserver(this);
        }

        public void AddToObserve(IContextActionButton target)
        {
            target.OnButtonActivation += TryCloseBackgroundScreen;
        }

        public void RemoveFromObserve(IContextActionButton target)
        {
            target.OnButtonActivation -= TryCloseBackgroundScreen;
        }

        private void TryCloseBackgroundScreen(IContextAction contextAction)
        {
            if (!contextAction.closeBackgroundScreen) return;
            _backgroundScreen.Close();
        }
    }
}


