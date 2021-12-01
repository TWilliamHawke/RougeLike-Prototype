using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.Player;
using Entities.AI;
using System;
using Core.Input;
using Core;

namespace Entities
{
    public class EntitiesManager : MonoBehaviour
    {
        [SerializeField] PlayerCore _player;
        [SerializeField] Enemy _testEnemy;
        [SerializeField] InputController _inputController;
        [SerializeField] GameObjects _gameObjects;

        StateMachine _testStateMachine;

        StateMachine _currentStateMachine;
        TurnStage _currentTurnStage = TurnStage.playerTurn;

        Stack<StateMachine> _activeEnemies = new Stack<StateMachine>();

        public void StartUp()
        {
            _player.Init();
            _testEnemy.Init();
            _testStateMachine = _testEnemy.stateMachine;
            _gameObjects.OnEntityTurnEnd += ChangeActiveEntity;
            _testStateMachine.Init();
        }

        void OnDestroy()
        {
            _gameObjects.OnEntityTurnEnd -= ChangeActiveEntity;
        }

        void ChangeActiveEntity()
        {
            if (_currentTurnStage == TurnStage.playerTurn)
            {
                _currentTurnStage = TurnStage.enemyTurn;
                AddActiveEnemiesToStack();
                StartNextEnenyTurn();
            }
            else
            {
                StartNextEnenyTurn();
            }
        }

        void StartNextEnenyTurn()
        {
            _currentStateMachine?.EndTurn();

            if (_activeEnemies.Count > 0)
            {
                _currentStateMachine = _activeEnemies.Pop();
                _currentStateMachine.StartTurn();
				Debug.Log(_activeEnemies.Count);
            }
            else
            {
                _currentStateMachine = null;
                _currentTurnStage = TurnStage.playerTurn;
                _inputController.EnableLeftClick();
				_player.StartTurn();
            }
        }

        void AddActiveEnemiesToStack()
        {
            _activeEnemies.Push(_testStateMachine);
        }

        enum TurnStage
        {
            playerTurn,
            enemyTurn,
        }
    }
}