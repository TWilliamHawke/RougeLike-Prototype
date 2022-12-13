using System.Collections;
using System.Collections.Generic;
using Entities.PlayerScripts;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Objects
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class MapObject : MonoBehaviour, IMapObject
    {
        public event UnityAction<IMapObject> OnPlayerEnter;
        public event UnityAction<IMapObject> OnPlayerExit;

        public IIconData template => _template;

        public IMapObjectBehavior behavior => _behavior;

        IMapObjectTemplate _template;
        IMapObjectBehavior _behavior;
        RandomStack<Vector3Int> _tileStorage;

        int _posX;
        int _posY;

        private void Awake()
        {
            _behavior = GetComponent<IMapObjectBehavior>();
        }

        public RandomStack<Vector3Int> GetWalkableTiles()
        {
            return _tileStorage;
        }

        public void BindTemplate(IMapObjectTemplate template)
        {
            _template = template;
            _posX = (int)transform.position.x;
            _posY = (int)transform.position.y;
            GetComponent<BoxCollider2D>().size = new Vector2(template.width, template.height);
            FillStorageWithWalkableTile();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.GetComponent<Player>()) return;
            OnPlayerEnter?.Invoke(this);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.GetComponent<Player>()) return;
            OnPlayerExit?.Invoke(this);
        }

        private void FillStorageWithWalkableTile()
        {
            int tilesCount = _template.width * _template.height;
            if (!_template.centerZoneIsWalkable)
            {
                tilesCount -= _template.centerZoneWidth * _template.centerZoneHeight;
            }

            _tileStorage = new RandomStack<Vector3Int>(tilesCount);

            for (int x = _posX - _template.width / 2; x <= _posX + _template.width / 2; x++)
            {
                for (int y = _posY - _template.height / 2; y <= _posY + _template.height / 2; y++)
                {
                    if (!TileIsWalkable(x, y)) continue;
                    _tileStorage.Push(new Vector3Int(x, y, 0));
                }
            }
        }

        private bool TileIsWalkable(int x, int y)
        {
            if (_template.centerZoneIsWalkable) return true;

            if (x < _posX - _template.centerZoneWidth / 2 || x >= _posX + 1 + _template.centerZoneWidth / 2) return true;
            if (y < _posY - _template.centerZoneHeight / 2 || y >= _posY + 1 + _template.centerZoneHeight / 2) return true;

            return false;
        }
    }
}

