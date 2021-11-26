using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities.Player;
using Entities.AI;

namespace Entities
{
	public class EntitiesManager : MonoBehaviour
	{
	    [SerializeField] PlayerCore _player;
		[SerializeField] Enemy _testEnemy;

		StateMachine _testStateMachine;

		StateMachine _currentStateMachine;

		public void StartUp()
		{
			_player.Init();
			_testEnemy.Init();
			_player.OnPlayerTurnEnd += StartEnemyTurn;
			_testStateMachine = _testEnemy.stateMachine;
			_testStateMachine.Init();
		}

		void StartEnemyTurn()
		{
			_currentStateMachine = _testStateMachine;
			_currentStateMachine.StartTurn();
		}

		private void Update() {
			if(_currentStateMachine == null) return;

			if(_currentStateMachine.isDone)
			{
				_currentStateMachine.EndTurn();
				_currentStateMachine = null;
			}
		}
	}
}