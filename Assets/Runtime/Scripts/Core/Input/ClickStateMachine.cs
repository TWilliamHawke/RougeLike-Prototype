using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using Entities.PlayerScripts;
using Map;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Input
{
    public class ClickStateMachine : IInjectionTarget, IInfoModeState
    {
        [InjectField] InputController _inputController;
        [InjectField] InfoButton _infoButton;
        [InjectField] TilesGrid _tileGrid;

        Player _player;

        bool _infoMode = false;
        public bool infoMode => _infoMode;

        public ClickStateMachine(GameObjects gameObjects)
        {
            _player = gameObjects.player;
        }

        List<IMouseClickState> _clickStates = new List<IMouseClickState>();


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
            _clickStates.Add(new ClickUnwalkableTile(
                inputController: _inputController,
                tilemapController: _tileGrid));

            _clickStates.Add(new ClickPlayer(_inputController));

            _clickStates.Add(new ClickRangeAttackTarget(
                inputController: _inputController,
                player: _player.GetComponent<ProjectileController>()));
            _clickStates.Add(new ClickRemoteObject(_inputController, _player));
            _clickStates.Add(new ClickNextTileObject(_inputController, _player));

            //tile hasn't any objects
            _clickStates.Add(new ClickWalkableTile(
                inputController: _inputController,
                tileGrid: _tileGrid,
                player: _player));
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

    public interface IInfoModeState
    {
        bool infoMode { get; }
    }
}