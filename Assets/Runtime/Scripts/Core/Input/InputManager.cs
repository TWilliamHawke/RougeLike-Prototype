using Abilities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Input
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        InputController _inputController;
        ClickStateMachine _clickStateMachine;

        [SerializeField] QuickBarDataStorage _quickBarDataStorage;
        [SerializeField] Injector _inputControllerInjector;
        [SerializeField] Injector _playerInjector;
        [SerializeField] Injector _stateMachineInjector;
        [SerializeField] Injector _hoveredTileInjector;

        void OnDestroy()
        {
            _clickStateMachine.Unsubscribe();
        }

        private void Awake()
        {
            _inputController = new InputController();
            _inputControllerInjector.SetDependency(_inputController);

            DefaultClickActions actionList = new(_quickBarDataStorage);
            _playerInjector.AddInjectionTarget(actionList);

            _clickStateMachine = new(actionList);
            _inputControllerInjector.AddInjectionTarget(_clickStateMachine);
            _hoveredTileInjector.AddInjectionTarget(_clickStateMachine);
            _stateMachineInjector.SetDependency(_clickStateMachine);
        }

        //used in editor
        public void EnableLeftClick()
        {
            _inputController.EnableLeftClick();
        }

    }
}