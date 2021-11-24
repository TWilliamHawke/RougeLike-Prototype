using System.Collections;
using System.Collections.Generic;
using Core;
using Map;
using UnityEngine;
using Entities.Behavior;
using Entities.Combat;

namespace Entities.PlayerScripts
{
    public class Player : MonoBehaviour, ICanMove, IAttackTarget, ICanAttack
    {
        [SerializeField] GameObjects _gameObjects;
        [SerializeField] TilemapController _mapController;
        [SerializeField] VisibilityController _visibilityController;
        [SerializeField] PlayerStats _stats;
        [SerializeField] Inventory _inventory;        
        [SerializeField] MovementController _movementController;
        [SerializeField] MeleeAttackController _meleeAttackController;

        TileNode _currentNode;
        IInteractive _interactiveTarget;

        public TileNode currentNode => _currentNode;
        public Dictionary<DamageType, int> resists => _stats.CalculateCurrentResists();
        public IDamageSource damageSource => _stats.CalculateDamageData();


        public void Init()
        {
            _visibilityController.ChangeViewingRange();
            _movementController.Init(this);
            _meleeAttackController.Init(this);
        }

        public void Attack(IAttackTarget target)
        {
            _meleeAttackController.StartAttack(target);
        }

        public void MoveTo(TileNode node)
        {
            Vector3 position = node.position2d.AddZ(0);
            TeleportTo(position);
            _currentNode = node;
            _movementController.SuspendMovement();

            if(_interactiveTarget != null && _movementController.pathLength <= 1)
            {
                _interactiveTarget.Interact(this);
                _interactiveTarget = null;
            }
            else
            {
                _movementController.TakeAStep();
            }
        }

        public void Goto(TileNode node)
        {
            _movementController.SetDestination(node);
            _movementController.TakeAStep();
        }

        public void InteractWith(IInteractive obj)
        {
            if(_mapController.TryGetNode(obj.transform.position.ToInt(), out var node))
            {
                _interactiveTarget = obj;
                _movementController.SetDestination(node);

                if(_movementController.pathLength <= 1)
                {
                    obj.Interact(this);
                    _interactiveTarget = null;
                }
                else
                {
                    _movementController.TakeAStep();
                }

            }
        }



        public void SpawnAt(TileNode node)
        {
            Vector3 position = node.position2d.AddZ(0);
            TeleportTo(position);
            _currentNode = node;
        }

        public void TeleportTo(Vector3 position)
        {
            transform.position = transform.position.ChangeXYFrom(position);
            _gameObjects.mainCamera.CenterAt(position);

        }

        public void TakeDamage(int damage)
        {
            Debug.Log($"Damage taken: {damage} hp");
        }
    }
}