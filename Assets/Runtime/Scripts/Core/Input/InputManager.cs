using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        InputController _inputController;
        ClickStateMachine _clickStateMachine;

        [SerializeField] Injector _inputControllerInjector;
        [SerializeField] Injector _tileGridInjector;
        [SerializeField] Injector _playerInjector;
        [SerializeField] Injector _stateMachineInjector;

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
            _inputControllerInjector.SetDependency(_inputController);

            DefaultClickActions actionList = new();
            _playerInjector.AddInjectionTarget(actionList);
            _inputControllerInjector.AddInjectionTarget(actionList);
            _tileGridInjector.AddInjectionTarget(actionList);

            _clickStateMachine = new(actionList);
            _inputControllerInjector.AddInjectionTarget(_clickStateMachine);
            _stateMachineInjector.SetDependency(_clickStateMachine);
        }

        //used in editor
        public void EnableLeftClick()
        {
            _inputController.EnableLeftClick();
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