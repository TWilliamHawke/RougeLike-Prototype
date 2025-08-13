using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Core.Input.NewInput;

namespace Core.Input
{
    public class InputController
    {
        public event UnityAction<ActionMap> OnActionMapChange;

        NewInput _newInput;
        ActionMap _currentActionMap = ActionMap.Disabled;

        //action maps
        public UIActions ui => _newInput.UI;
        public MainActions main => _newInput.Main;
        public DisabledActions disabled => _newInput.Disabled;
        public TargetSelectionActions targetSelection => _newInput.TargetSelection;

        List<InputActionMap> _actionMaps = new List<InputActionMap>();

        public InputController()
        {
            _newInput = new NewInput();

            _actionMaps.Add(_newInput.Main.Get());
            _actionMaps.Add(_newInput.UI.Get());
            _actionMaps.Add(_newInput.Disabled.Get());
            _actionMaps.Add(_newInput.TargetSelection.Get());

            _newInput.Main.Enable();
            _currentActionMap = ActionMap.Main;
        }

        public void SwitchToMainActionMap()
        {
            SwitchActionMap(ActionMap.Main);
        }

        public void SwitchToUIActionMap()
        {
            SwitchActionMap(ActionMap.UI);
        }

        public void SwitchToTargetSelection()
        {
            SwitchActionMap(ActionMap.TargetSelection);
        }

        //disable all but escape
        public void DisableControl()
        {
            SwitchActionMap(ActionMap.Disabled);
        }

        public void DisableLeftClick()
        {
            _newInput.Main.Click.Disable();
        }

        public void EnableLeftClick()
        {
            _newInput.Main.Click.Enable();
        }

        private void SwitchActionMap(ActionMap newActionMap)
        {
            if (_currentActionMap == newActionMap) return;

            foreach (var actionMap in _actionMaps)
            {
                if (actionMap.name == newActionMap.ToString())
                {
                    actionMap.Enable();
                    _currentActionMap = newActionMap;
                    OnActionMapChange?.Invoke(newActionMap);
                    continue;
                } //else
                actionMap.Disable();
            }
        }
    }

    public enum ActionMap
    {
        Disabled,
        Main,
        UI,
        TargetSelection
    }
}