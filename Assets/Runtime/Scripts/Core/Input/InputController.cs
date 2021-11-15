using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static Core.NewInput;

namespace Core
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
		public Vector3Int hoveredTilePos => _hoveredTilePos;

		const string MAIN_ACTIONS_ID = "Main";
		const string UI_ACTIONS_ID = "UI";

		public void Init(PlayerInput playerInput)
		{
			_newInput = new NewInput();
			_newInput.Main.Enable();
			_currentActionMap = ActionMap.main;
			_playerInput = playerInput;
		}

		public void Clear()
		{
			_playerInput = null;
			OnHoveredTileChange = null;
		}

		public void SwitchToMainActionMap()
		{
			SwitchActionMap(MAIN_ACTIONS_ID, ActionMap.main);
		}

		public void SwitchToUIActionMap()
		{
			SwitchActionMap(UI_ACTIONS_ID, ActionMap.ui);
		}

		public void UpdatePointerPosition(Vector2 position)
        {
            Vector3Int newTilePos = position.Toint().AddZ(0);

            if (newTilePos == _hoveredTilePos)return;

            _hoveredTilePos = newTilePos;
            OnHoveredTileChange?.Invoke(newTilePos);

        }

        void SwitchActionMap(string stringID, ActionMap enumID)
		{
			if(_currentActionMap == enumID) return;
			_playerInput.SwitchCurrentActionMap(stringID);
			_currentActionMap = enumID;
		}


		enum ActionMap
		{
			main,
			ui
		}
	}
}