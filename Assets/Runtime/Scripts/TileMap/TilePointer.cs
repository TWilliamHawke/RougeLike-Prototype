using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Input;

namespace Map
{
    public class TilePointer : MonoBehaviour
    {
        [SerializeField] InputController _input;
        [SerializeField] Color _defaultColor = Color.yellow;
        [SerializeField] Color _enemyColor = Color.red;
        [SerializeField] SpriteRenderer _sprite;

        void Awake()
        {
            _input.OnHoveredTileChange += ChangePosition;
            _sprite.color = _defaultColor;
        }

        void OnDestroy()
        {
            _input.OnHoveredTileChange -= ChangePosition;
        }

        void ChangePosition(Vector3Int position)
        {
            transform.position = transform.position.ChangeXYFrom(position);
        }

    }
}