using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using Entities.PlayerScripts;
using Map;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Input
{
    public class ClickStateMachine : IInjectionTarget, IClickStateSource, IInfoModeState
    {
        [InjectField] InputController _inputController;
        [InjectField] InfoButton _infoButton;

        TilemapController _tilemapController;
        Player _player;

        bool _infoMode = false;
        public bool infoMode => _infoMode;

        public ClickStateMachine(GameObjects gameObjects)
        {
            _tilemapController = gameObjects.tilemapController;
            _player = gameObjects.player;
        }

        List<IMouseClickState> _clickStates = new List<IMouseClickState>();

        InputController IClickStateSource.inputController => _inputController;
        TilemapController IClickStateSource.tilemapController => _tilemapController;
        Player IClickStateSource.player => _player;

        bool IInjectionTarget.waitForAllDependencies => true;

        void OnDestroy()
        {
            if (_inputController is null) return;
            _inputController.main.Click.started -= CheckTileObjects;
        }

        void IInjectionTarget.FinalizeInjection()
        {
            _inputController.main.Click.started += CheckTileObjects;
            _infoButton.OnClick += ToggleInfoMode;

            FillStateMachine();
        }

        private void FillStateMachine()
        {
            //check ui click before tiles
            _clickStates.Add(new ClickUI());
            _clickStates.Add(new ClickObjectInfo(this, _inputController));
            //check unwalkable before gameobjects
            _clickStates.Add(new ClickUnwalkableTile(this));

            _clickStates.Add(new ClickPlayer(this));

            _clickStates.Add(new ClickRangeAttackTarget(this));
            _clickStates.Add(new ClickRemoteObject(this));
            _clickStates.Add(new ClickNextTileObject(this));

            //tile hasn't any objects
            _clickStates.Add(new ClickWalkableTile(this));
        }

        private void ToggleInfoMode()
        {
            _infoMode = !_infoMode;
            _infoButton.ToggleButton(infoMode);
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

    public interface IClickStateSource
    {
        InputController inputController { get; }
        TilemapController tilemapController { get; }
        Player player { get; }
    }

    public interface IInfoModeState
    {
        bool infoMode { get; }
    }
}