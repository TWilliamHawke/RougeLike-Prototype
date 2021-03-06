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


        StateMachine _currentStateMachine;

        Stack<StateMachine> _activeEnemies = new Stack<StateMachine>();

        public void StartUp()
        {
            _player.Init();
            _testEnemy.Init();
            _testEnemy.stateMachine.Init();
            _testEnemy.stateMachine.OnTurnEnd += StartNextEnemyTurn;
            _player.OnPlayerTurnEnd += StartFirstEnemyTurn;
        }


        void StartFirstEnemyTurn()
        {
            AddActiveEnemiesToStack();
            StartNextEnemyTurn();
        }

        void StartNextEnemyTurn()
        {
            if (_activeEnemies.Count > 0)
            {
                _currentStateMachine = _activeEnemies.Pop();
                _currentStateMachine.StartTurn();
            }
            else
            {
                _currentStateMachine = null;
                _inputController.EnableLeftClick();
                _player.StartTurn();
            }
        }

        void AddActiveEnemiesToStack()
        {
            _activeEnemies.Push(_testEnemy.stateMachine);
        }

    }
}