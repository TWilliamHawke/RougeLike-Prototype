using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class RadialContextMenu : MonoBehaviour, IContextMenu
    {
        [SerializeField] UIScreen _menu;
        [SerializeField] Injector _selfInjector;

        [SerializeField] RadialContextButton[] _buttons;

        [InjectField] ModalWindowController _modalWindowController;

        Dictionary<RadialButtonPosition, RadialContextButton> _buttonsByPosition = new();

        void Awake()
        {
            _selfInjector.SetDependency(this);

            foreach (var button in _buttons)
            {
                _buttonsByPosition[button.buttonPosition] = button;
            }
        }

        public void Fill(IEnumerable<IContextAction> actionsList)
        {
            foreach (var button in _buttons)
            {
                button.ClearAction();
            }

            foreach (var action in actionsList)
            {
                if (action is not IRadialMenuAction) continue;
                BindAction(action);
            }
        }

        private void BindAction(IContextAction action)
        {
            var preferedPosition = (action as IRadialMenuAction).preferedPosition;
            var button = _buttonsByPosition[preferedPosition];
            button?.BindAction(action);
        }
    }
}


