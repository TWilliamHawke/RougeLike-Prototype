using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Entities;

namespace Core.Input
{
    public class TilemapClickController : MonoBehaviour
    {
        [SerializeField] InputController _inputController;
        [SerializeField] GameObjects _gameObjects;

        List<IMouseClickState> _clickStates = new List<IMouseClickState>();

        public void StartUp()
        {
            _inputController.main.Click.started += CheckTileObjects;

            //check ui click before tiles
            _clickStates.Add(new ClickUI());
            //check unwalkable before gameobjects
            _clickStates.Add(new ClickUnwalkableTile(_gameObjects));

            _clickStates.Add(new ClickPlayer(_gameObjects));


            _clickStates.Add(new ClickRemoteObject(_gameObjects));
            _clickStates.Add(new ClickNextTileObject(_gameObjects));

            //tile hasn't any objects
            _clickStates.Add(new ClickWalkableTile(_gameObjects));
        }

        void OnDestroy()
        {
            _inputController.main.Click.started -= CheckTileObjects;
        }

        void CheckTileObjects(InputAction.CallbackContext _)
        {
            foreach (var state in _clickStates)
            {
                if (!state.Condition()) continue;

                state.ProcessClick();
                return;
            }
        }
    }
}