using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities.AI
{
    public class AIManager : MonoBehaviour, IObserver<Entity>
    {
        Stack<StateMachine> _activeEnemies = new();
        List<StateMachine> _allEnemies = new();

        [InjectField] EntitiesManager _entitiesManager;
        [SerializeField] CustomEvent _onPlayerTurnStart;

        public void AddToObserve(Entity target)
        {
            _allEnemies.Add(target.stateMachine);
        }

        public void RemoveFromObserve(Entity target)
        {
        }

        //used in editor
        public void StartFirstEnemyTurn()
        {
            AddActiveEnemiesToStack();
            StartNextEnemyTurn();
        }

        //used in editor
        public void StartNextEnemyTurn()
        {
            if (_activeEnemies.Count > 0)
            {
                var currentStateMachine = _activeEnemies.Pop();
                currentStateMachine.StartTurn();
            }
            else
            {
                _onPlayerTurnStart.Invoke();
            }
        }

        //used in editor
        public void StartObservation()
        {
            _entitiesManager.AddObserver(this);
        }

        void AddActiveEnemiesToStack()
        {
            //logic will be much more complex
            _allEnemies.ForEach(enemy => _activeEnemies.Push(enemy));
        }

    }
}
