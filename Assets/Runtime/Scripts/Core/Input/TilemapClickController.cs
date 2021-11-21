using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Entities;

namespace Core
{
    public class TilemapClickController : MonoBehaviour
    {
        [SerializeField] InputController _inputController;
        [SerializeField] GameObjects _gameObjects;
        [SerializeField] TilemapController _tilemapController;

        public void StartUp()
        {
            _inputController.main.Click.started += CheckTileObjects;
        }

        void OnDestroy()
        {
            _inputController.main.Click.started -= CheckTileObjects;
        }

        void CheckTileObjects(InputAction.CallbackContext _)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            Vector3Int position = _inputController.hoveredTilePos;

            var hits = Physics2D.RaycastAll(position.RemoveZ(), Vector2.zero);

            foreach (var hit in hits)
            {
                if(hit.collider.TryGetComponent<IInteractive>(out var obj))
                {
                    _gameObjects.player.InteractWith(obj);
                    return;
                }
            }

            if (_tilemapController.TryGetNode(position, out var node))
            {
                if (node.isWalkable)
                {
                    //_gameObjects.player.MoveTo(node);
                    _gameObjects.player.Goto(node);
                }
                else
                {
                    Debug.Log("Unwalkable");
                }
            }

        }
    }
}