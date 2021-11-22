using System.Collections;
using System.Collections.Generic;
using Core;
using Map;
using UnityEngine;
using Entities.Behavior;

namespace Entities.PlayerScripts
{
    public class Player : MonoBehaviour, ICanMove
    {
        [SerializeField] GameObjects _gameObjects;
        [SerializeField] TilemapController _mapController;
        [SerializeField] VisibilityController _visibilityController;        

        TileNode _currentNode;
        IInteractive _interactiveTarget;
        MovementController _movementController;

        public TileNode currentNode => _currentNode;

        private void Update() {
            _movementController?.UpdatePosition();
        }


        public void Init()
        {
            _movementController = new MovementController(this, _mapController);
            _visibilityController.ChangeViewingRange();
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

    }
}