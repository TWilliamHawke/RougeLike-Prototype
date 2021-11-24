using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Core.Input.NewInput;

namespace Core.Input
{
    public class InputController : ScriptableObject
    {
        public event UnityAction<Vector3Int> OnHoveredTileChange;

        NewInput _newInput;
        PlayerInput _playerInput;
        Vector3Int _hoveredTilePos;
        ActionMap _currentActionMap;

        public UIActions ui => _newInput.UI;
        public MainActions main => _newInput.Main;
        public DisabledActions disabled => _newInput.Disabled;
        public Vector3Int hoveredTilePos => _hoveredTilePos;

        public void Init(PlayerInput playerInput)
        {
            _newInput = new NewInput();
            _newInput.Main.Enable();
            _currentActionMap = ActionMap.Main;
            _playerInput = playerInput;
        }

        public void Clear()
        {
            _playerInput = null;
            OnHoveredTileChange = null;
        }

        public void SwitchToMainActionMap()
        {
            SwitchActionMap(ActionMap.Main);
        }

        public void SwitchToUIActionMap()
        {
            SwitchActionMap(ActionMap.UI);
        }

        //disable all but escape
        public void DisableControl()
        {
            SwitchActionMap(ActionMap.Disabled);
        }

        public void UpdatePointerPosition(Vector2 position)
        {
            Vector3Int newTilePos = position.Toint().AddZ(0);

            if (newTilePos == _hoveredTilePos) return;

            _hoveredTilePos = newTilePos;
            OnHoveredTileChange?.Invoke(newTilePos);

        }

        public void DisableLeftClick()
        {
            _newInput.Main.Click.Disable();
        }

        public void EnableLeftClick()
        {
            _newInput.Main.Click.Enable();
        }

        void SwitchActionMap(ActionMap actionMap)
        {
            if (_currentActionMap == actionMap) return;
            _playerInput.SwitchCurrentActionMap(actionMap.ToString());
            _currentActionMap = actionMap;
        }


        enum ActionMap
        {
            Main,
            UI,
            Disabled,
        }
    }
}