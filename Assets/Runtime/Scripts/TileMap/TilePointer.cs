using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Map
{
    public class TilePointer : MonoBehaviour
    {
        [SerializeField] InputController _input;
        [SerializeField] Color _defaultColor = Color.yellow;
        [SerializeField] Color _enemyColor = Color.red;
        [SerializeField] SpriteRenderer _sprite;

        public void Subscribe()
        {
            _input.main.Click.started += ShowTile;
            _input.OnHoveredTileChange += ChangePosition;
            _sprite.color = _defaultColor;
        }

        void OnDestroy()
        {
            _input.main.Click.started -= ShowTile;
            _input.OnHoveredTileChange -= ChangePosition;
        }

        void ChangePosition(Vector3Int position)
        {
            transform.position = transform.position.ChangeXYFrom(position);
        }

        void ShowTile(InputAction.CallbackContext context)
        {
            Debug.Log("Click;");
        }

    }
}