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

        [InjectField] MapZonesObserver _mapZonesObserver;

        IIconData _template;

        int _posX => (int)transform.position.x;
        int _posY => (int)transform.position.y;

        public string displayName => _template.displayName;
        public Sprite icon => _template.icon;

        public abstract IMapActionList mapActionList { get; }
        public abstract TaskData currentTask { get; }

        public void BindTemplate(IMapZoneTemplate template)
        {
            _template = template;
            GetComponent<BoxCollider2D>().size = template.size;
        }

        protected void AddToObserver()
        {
            _mapZonesObserver.AddToObserve(this);
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

    }
}

