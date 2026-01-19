using Map;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Input
{
    public class ClickStateMachine : IInjectionTarget
    {
        [InjectField] InputController _inputController;
        [InjectField] TilesGrid _tilemapController;
        [InjectField] ScreenPositionReader _screenPositionReader;

        IClickActionList _defaultClickActions;
        IClickActionList _currentClickActions;

        bool IInjectionTarget.waitForAllDependencies => true;

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
            _inputController.main.Click.started -= ProcessMouseClick;
            _inputController.main.Touch.started -= ProcessScreenTouch;
            _currentClickActions.CleanUp();
            _defaultClickActions.CleanUp();
        }

        void IInjectionTarget.FinalizeInjection()
        {
            _inputController.main.Click.started += ProcessMouseClick;
            _inputController.main.Touch.started += ProcessScreenTouch;
        }

        void ProcessScreenTouch(InputAction.CallbackContext _)
        {
            _screenPositionReader.SwitchToTouchReader();
            ProcessClick();
        }

        void ProcessMouseClick(InputAction.CallbackContext _)
        {
            _screenPositionReader.SwitchToMouseReader();
            ProcessClick();
        }

        void ProcessClick()
        {
            var screenPosition = _screenPositionReader.ReadScreenPosition();
            var worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            var hit = Physics2D.Raycast(worldPosition, Vector2.zero);
            if (!hit) return;
            Vector3Int gridPos = hit.point.Toint().AddZ(0);

            if (_tilemapController.TryGetNode(gridPos, out var tile))
            {
                ProcessClick(tile);
            }
        }

        void ProcessClick(TileNode node)
        {
            foreach (var state in _currentClickActions.GetActions())
            {
                if (!state.CanBeUsedOnTile(node)) continue;
                state.ProcessClick(node);
                return;
            }
        }
    }
}