using System.Collections;
using System.Collections.Generic;
using Entities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Map
{
    public class HoveredTileObserver: MonoBehaviour
    {
        public event UnityAction<TileNode> OnHoveredTileChange;

        TileNode _hoveredTile;

        public IEnumerable<IObstacleEntity> entitiesOnTile => _hoveredTile.entitiesOnTile;
        public Vector3Int tilePos => _hoveredTile.position;
        public ITileClickData hoveredTile => _hoveredTile;

        [InjectField] TilesGrid _tilemapController;


        void Update()
        {
            if (_hoveredTile is null) return;
            UpdateHoveredTile();
        }

        //used in editor
        public void SelectFirstTile()
        {
            _tilemapController.TryGetNodeAt(0, 0, out _hoveredTile);
        }

        private void UpdateHoveredTile()
        {
            var startPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var hit = Physics2D.Raycast(startPoint, Vector2.zero);
            if (!hit) return;

            Vector3Int newTilePos = hit.point.Toint().AddZ(0);

            if (newTilePos == tilePos) return;
            if (_tilemapController.TryGetNode(newTilePos, out var tile))
            {
                _hoveredTile = tile;
                OnHoveredTileChange?.Invoke(_hoveredTile);
            }

        }
    }
}