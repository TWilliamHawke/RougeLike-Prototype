using System.Collections;
using System.Collections.Generic;
using Entities.Combat;
using Entities.Player;
using Map;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core.Input
{
    public class ClickStateMachine : IInjectionTarget, IClickStateSource
    {
        [InjectField] InputController _inputController;

        TilemapController _tilemapController;
        PlayerCore _player;

        public ClickStateMachine(GameObjects gameObjects)
        {
            _tilemapController = gameObjects.tilemapController;
            _player = gameObjects.player;
        }

        List<IMouseClickState> _clickStates = new List<IMouseClickState>();

        InputController IClickStateSource.inputController => _inputController;
        TilemapController IClickStateSource.tilemapController => _tilemapController;
        PlayerCore IClickStateSource.player => _player;

        bool IInjectionTarget.waitForAllDependencies => true;

        void StartUp()
        {
            _inputController.main.Click.started += CheckTileObjects;

            //check ui click before tiles
            _clickStates.Add(new ClickUI());
            //check unwalkable before gameobjects
            _clickStates.Add(new ClickUnwalkableTile(this));

            _clickStates.Add(new ClickPlayer(this));

            _clickStates.Add(new ClickRangeAttackTarget(this));
            _clickStates.Add(new ClickRemoteObject(this));
            _clickStates.Add(new ClickNextTileObject(this));

            //tile hasn't any objects
            _clickStates.Add(new ClickWalkableTile(this));
        }

        void OnDestroy()
        {
            if (_inputController is null) return;
            _inputController.main.Click.started -= CheckTileObjects;
        }

        void IInjectionTarget.FinalizeInjection()
        {
            StartUp();
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
        PlayerCore player { get; }
    }
}