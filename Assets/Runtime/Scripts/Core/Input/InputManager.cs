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
        [SerializeField] Injector _infoButtonInjector;
        [SerializeField] Injector _tileGridInjector;
        [SerializeField] Injector _playerInjector;

        void Update()
        {
            UpdateHoveredTilePosition();
        }

        void OnDestroy()
        {
            _clickStateMachine.Unsubscribe();
            _inputController.Clear();
        }

        private void Awake()
        {
            _inputController = new InputController();
            _inputControllerInjector.AddDependency(_inputController);

            _clickStateMachine = new ClickStateMachine();
            _playerInjector.AddInjectionTarget(_clickStateMachine);
            _inputControllerInjector.AddInjectionTarget(_clickStateMachine);
            _tileGridInjector.AddInjectionTarget(_clickStateMachine);
            _infoButtonInjector.AddInjectionTarget(_clickStateMachine);
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