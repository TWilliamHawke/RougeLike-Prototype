using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Core
{
    public class TilemapClickController : MonoBehaviour
    {
        [SerializeField] InputController _inputController;
        [SerializeField] GameObjects _gameObjects;

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

            _gameObjects.player.TeleportTo(_inputController.hoveredTilePos);
        }
    }
}