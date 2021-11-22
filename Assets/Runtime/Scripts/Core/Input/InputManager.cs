using System.Collections;
using System.Collections.Generic;
using Map;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Core
{
    [RequireComponent(typeof(PlayerInput))]
    public class InputManager : MonoBehaviour
    {
        [SerializeField] InputController _inputController;
        [SerializeField] TilePointer _tileSelector;
        [SerializeField] TilemapClickController _clickController;

        void Update()
        {
            UpdateHoveredTilePosition();
        }

        void OnDestroy()
        {
            _inputController.Clear();
        }

        public void StartUp()
        {
            var playerInput = GetComponent<PlayerInput>();
            _inputController.Init(playerInput);

            // requires InputController
            _tileSelector.Subscribe();
            _clickController.StartUp();
        }

        void UpdateHoveredTilePosition()
        {
            //if (EventSystem.current.IsPointerOverGameObject()) return;

            var startPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var hit = Physics2D.Raycast(startPoint, Vector2.zero);
            if (!hit) return;
            _inputController.UpdatePointerPosition(hit.point);
        }


    }
}