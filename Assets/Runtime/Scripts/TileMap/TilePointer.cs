using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Input;

namespace Map
{
    public class TilePointer : MonoBehaviour
    {
        [SerializeField] Color _defaultColor = Color.yellow;
        [SerializeField] Color _enemyColor = Color.red;
        [SerializeField] SpriteRenderer _sprite;

        [InjectField] HoveredTileObserver _hoveredTileObserver;

        void Awake()
        {
            _sprite.color = _defaultColor;
        }

        void OnDestroy()
        {
            if(_hoveredTileObserver is null) return;
            _hoveredTileObserver.OnHoveredTileChange -= ChangePosition;
        }

        void ChangePosition(TileNode node)
        {
            transform.position = transform.position.ReplaceXYFrom(node.position);
        }

        public void FinalizeInjection()
        {
            _hoveredTileObserver.OnHoveredTileChange += ChangePosition;
        }
    }
}