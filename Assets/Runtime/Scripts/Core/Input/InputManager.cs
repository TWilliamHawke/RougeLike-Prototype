using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Core.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        InputController _inputController;
        ClickStateMachine _clickStateMachine;
        [SerializeField] Injector _inputControllerInjector;
        [SerializeField] GameObjects _gameObjects;

        void Update()
        {
            UpdateHoveredTilePosition();
        }

        void OnDestroy()
        {
            _inputController.Clear();
        }

        public void StartUp()
        {
            _inputController = new InputController();
            _clickStateMachine = new ClickStateMachine(_gameObjects);
            _inputControllerInjector.AddDependency(_inputController);
            _inputControllerInjector.AddInjectionTarget(_clickStateMachine);
        }

        void UpdateHoveredTilePosition()
        {
            //if (EventSystem.current.IsPointerOverGameObject()) return;

            var startPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var hit = Physics2D.Raycast(startPoint, Vector2.zero);
            if (!hit) return;
            _inputController.UpdatePointerPosition(hit.point);
        }


    }
}