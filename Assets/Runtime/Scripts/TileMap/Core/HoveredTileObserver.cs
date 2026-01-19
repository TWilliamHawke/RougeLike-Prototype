using System.Collections;
using System.Collections.Generic;
using Entities;
using Entities.PlayerScripts;
using Entities.Stats;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Map
{
    public class HoveredTileObserver : MonoBehaviour
    {
        public event UnityAction<TileNode> OnHoveredTileChange;

        TileNode _hoveredTile;

        public IEnumerable<IObstacleEntity> entitiesOnTile => _hoveredTile.entitiesOnTile;
        public Vector3Int tilePos => _hoveredTile.intPosition;
        public ITileClickData hoveredTile => _hoveredTile;

        [SerializeField] StaticStat _lineOfSightStat;
        [SerializeField] PlayerStats _playerStats;

        [InjectField] TilesGrid _tilemapController;
        [InjectField] Player _player;

        float _lineOfSightRadius;
        float _squaredLineOfSightRadius;

        void Update()
        {
            if (_hoveredTile is null) return;
            UpdateHoveredTile();
        }

        //used in editor
        public void SelectFirstTile()
        {
            _tilemapController.TryGetNodeAt(0, 0, out _hoveredTile);
            var lineOfSightContainer = _playerStats.FindContainer(_lineOfSightStat);
            lineOfSightContainer.OnFloatValueChanged += UpdateLineOfSightRadius;
            UpdateLineOfSightRadius(lineOfSightContainer.floatValue);
        }

        private void UpdateLineOfSightRadius(float newValue)
        {
            _lineOfSightRadius = newValue;
            _squaredLineOfSightRadius = _lineOfSightRadius * _lineOfSightRadius;
        }

        private void UpdateHoveredTile()
        {
            if (_tilemapController is null || _player is null) return;
            var startPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var hit = Physics2D.Raycast(startPoint, Vector2.zero);
            if (!hit) return;

            Vector3Int newTilePos = hit.point.Toint().AddZ(0);
            if (newTilePos == tilePos) return;

            Vector3Int playerPos = _player.transform.position.ToInt();
            int deltaX = newTilePos.x - playerPos.x;
            int deltaY = newTilePos.y - playerPos.y;
            if (deltaX * deltaX + deltaY * deltaY > _squaredLineOfSightRadius) return;

            if (_tilemapController.TryGetNode(newTilePos, out var tile))
            {
                _hoveredTile = tile;
                OnHoveredTileChange?.Invoke(_hoveredTile);
            }
        }
    }
}