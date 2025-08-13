using Map;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Input
{
    public class ClickStateMachine : IInjectionTarget
    {
        [InjectField] InputController _inputController;
        [InjectField] HoveredTileObserver _hoveredTileObserver;

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
                if (!state.CanBeUsedOnTile(_hoveredTileObserver.hoveredTile)) continue;
                state.ProcessClick(_hoveredTileObserver.hoveredTile);
                return;
            }
        }

    }
}