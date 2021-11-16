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

        TileNode _currentNode;
        MovementController _movementController;


        public TileNode currentNode => _currentNode;

        private void Update() {
            _movementController?.UpdatePosition();
        }


        public void Init()
        {
            _movementController = new MovementController(this);
        }

        public void MoveTo(TileNode node)
        {
            Vector3 position = node.position2d.AddZ(0);
            TeleportTo(position);
            _currentNode = node;
        }

        public void SetPath(Stack<TileNode> path)
        {
            _movementController.SetPath(path);
        }



        public void SpawnAt(TileNode node)
        {
            MoveTo(node);
        }

        public void TeleportTo(Vector3 position)
        {
            transform.position = transform.position.ChangeXYFrom(position);
            _gameObjects.mainCamera.CenterAt(position);

        }

    }
}