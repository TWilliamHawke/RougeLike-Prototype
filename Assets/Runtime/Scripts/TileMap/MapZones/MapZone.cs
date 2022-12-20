using System.Collections;
using System.Collections.Generic;
using Entities.PlayerScripts;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Zones
{
    [RequireComponent(typeof(BoxCollider2D))]
    public abstract class MapZone : MonoBehaviour, IMapZone
    {
        public event UnityAction<IMapZone> OnPlayerEnter;
        public event UnityAction<IMapZone> OnPlayerExit;

        protected IMapZoneTemplate _template;
        protected RandomStack<Vector3Int> _tileStorage;

        int _posX => (int)transform.position.x;
        int _posY => (int)transform.position.y;

        public string displayName => _template.displayName;
        public Sprite icon => _template.icon;

        public abstract IMapActionList mapActionList { get; }
        public abstract TaskData currentTask { get; }

        public void BindTemplate(IMapZoneTemplate template)
        {
            _template = template;
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

