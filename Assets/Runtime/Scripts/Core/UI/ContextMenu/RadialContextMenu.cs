using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class RadialContextMenu : MonoBehaviour, IContextMenu
    {
        [SerializeField] UIScreen _menu;

        [SerializeField] RadialContextButton[] _buttons;

        Dictionary<int, RadialContextButton> _buttonsByPosition = new();

        void Awake()
        {
            foreach (var button in _buttons)
            {
                _buttonsByPosition[button.buttonPosition] = button;
            }
        }

        public void Fill(IEnumerable<ContextActionContainer> actionsList)
        {
            foreach (var button in _buttons)
            {
                button.ClearAction();
            }

            foreach (var action in actionsList)
            {
                BindAction(action);
            }
        }

        public void AddButtonsObserver(IObserver<IContextActionButton> observer)
        {
            _buttons.ForEach(button => observer.AddToObserve(button));
        }

        public void CloseMenu()
        {
            _menu.Close();
        }

        private void BindAction(ContextActionContainer action)
        {
            var preferedPosition = action.preferedPosition;
            var button = _buttonsByPosition[preferedPosition];
            button?.BindAction(action);
        }
    }
}


