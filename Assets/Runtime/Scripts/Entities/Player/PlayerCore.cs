using System.Collections;
using System.Collections.Generic;
using Core;
using Map;
using UnityEngine;
using Entities.Behavior;
using Entities.Combat;
using UnityEngine.Events;

namespace Entities.Player
{
    [RequireComponent(typeof(VisibilityController))]
    public class PlayerCore : MonoBehaviour, ICanMove, IAttackTarget, ICanAttack
    {
        [SerializeField] GameObjects _gameObjects;
        [SerializeField] TilemapController _mapController;
        [SerializeField] PlayerStats _stats;
        [SerializeField] Inventory _inventory;
        [SerializeField] MovementController _movementController;
        [SerializeField] MeleeAttackController _meleeAttackController;


        public event UnityAction OnPlayerTurnEnd;

        TileNode _currentNode;
        IInteractive _target;

        public TileNode currentNode => _currentNode;
        public Dictionary<DamageType, int> resists => _stats.CalculateCurrentResists();
        public IDamageSource damageSource => _stats.CalculateDamageData();


        public void Init()
        {
            _movementController.Init(this);
            GetComponent<VisibilityController>().ChangeViewingRange();

            //stats requires player.transform
            _stats.player = this;
            
            _meleeAttackController.Init(this);
            _meleeAttackController.OnAttackEnd += () => OnPlayerTurnEnd?.Invoke();
        }

        public void Attack(IAttackTarget target)
        {
            _meleeAttackController.StartAttack(target);
        }

        public void GotoRemoteTarget(IInteractive target)
        {
            _target = target;
            _movementController.SetDestination(target);
            _movementController.TakeAStep();
        }

        public void GotoNode(TileNode node)
        {
            _movementController.SetDestination(node);
            _movementController.TakeAStep();
        }

        public void SpawnAt(TileNode node)
        {
            Vector3 position = node.position2d.AddZ(0);
            transform.position = position;
            _currentNode = node;
        }

        void ICanMove.ChangeNode(TileNode node)
        {
            Vector3 position = node.position2d.AddZ(0);
            _currentNode = node;
            _movementController.SuspendMovement();

            OnPlayerTurnEnd?.Invoke();

            if (_target != null && _movementController.pathLength <= 1)
            {
                _target.Interact(this);
                _target = null;
            }
            else
            {
                _movementController.TakeAStep();
            }
        }

        void IAttackTarget.TakeDamage(int damage)
        {
            _stats.DecreaseHealth(damage);
        }

    }
}