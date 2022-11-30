using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.UI
{
    public class RadialContextMenu : MonoBehaviour, IContextMenu
    {
        [SerializeField] UIScreen _canvas;
        [SerializeField] Injector _selfInjector;

		[SerializeField] RadialContextButton[] _buttons;

		Dictionary<RadialButtonPosition, RadialContextButton> _buttonsByPosition = new();

        private void Awake()
        {
			_selfInjector.SetDependency(this);

			foreach(var button in _buttons)
			{
				_buttonsByPosition[button.buttonPosition] = button;
			}
        }

        public void Fill(IEnumerable<IContextAction> actionsList)
        {
			foreach(var button in _buttons)
			{
				button.ClearAction();
			}

            foreach (var action in actionsList)
			{
				var button = _buttonsByPosition[action.preferedPosition];
				button?.BindAction(action);
			}
        }
    }
}


