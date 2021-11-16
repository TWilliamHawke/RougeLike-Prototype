using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Core
{
    public class TilemapClickController : MonoBehaviour
    {
        [SerializeField] InputController _inputController;
        [SerializeField] GameObjects _gameObjects;
        [SerializeField] TilemapController _tilemapController;

        public void StartUp()
        {
            _inputController.main.Click.started += MovePlayer;
        }

        void OnDestroy()
        {
            _inputController.main.Click.started -= MovePlayer;
        }

        void MovePlayer(InputAction.CallbackContext _)
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            Vector3Int position = _inputController.hoveredTilePos;

            if (_tilemapController.TryGetNode(position, out var node))
            {
                if (node.isWalkable)
                {
                    //_gameObjects.player.MoveTo(node);
                    var path = _tilemapController.FindPath(node);
                    if(path.Count > 0)
                    {
                        _gameObjects.player.SetPath(path);
                    }
                }
                else
                {
                    Debug.Log("Unwalkable");
                }
            }

        }
    }
}