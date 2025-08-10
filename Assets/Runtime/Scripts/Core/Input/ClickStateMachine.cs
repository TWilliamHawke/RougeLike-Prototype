using UnityEngine.InputSystem;

namespace Core.Input
{
    public class ClickStateMachine : IInjectionTarget
    {
        [InjectField] InputController _inputController;

        IClickActionList _defaultClickActions;
        IClickActionList _currentClickActions;

        bool IInjectionTarget.waitForAllDependencies => false;

        public ClickStateMachine(IClickActionList defaultClickActions)
        {
            _defaultClickActions = defaultClickActions;
            _currentClickActions = defaultClickActions;
        }

        public void ReplaceAcionList(IClickActionList actionList)
        {
            _currentClickActions = actionList;
        }

        public void ResetActionList()
        {
            _currentClickActions = _defaultClickActions;
        }

        public void Unsubscribe()
        {
            _inputController.main.Click.started -= CheckTileObjects;
        }

        void IInjectionTarget.FinalizeInjection()
        {
            _inputController.main.Click.started += CheckTileObjects;
        }

        void CheckTileObjects(InputAction.CallbackContext _)
        {
            foreach (var state in _currentClickActions)
            {
                if (!state.Condition()) continue;

                state.ProcessClick();
                return;
            }
        }

    }

    public interface IInfoModeState
    {
        bool infoMode { get; }
    }
}