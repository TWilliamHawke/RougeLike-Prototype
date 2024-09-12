using System.Collections;
using System.Collections.Generic;
using Entities.PlayerScripts;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Zones
{
    public abstract class MapZone : MonoBehaviour, IMapZone
    {
        public event UnityAction<IMapZone> OnPlayerEnter;
        public event UnityAction<IMapZone> OnPlayerExit;

        IIconData _template;

        public string displayName => _template.displayName;
        public Sprite icon => _template.icon;

        public abstract IMapActionList mapActionList { get; }
        public abstract TaskData currentTask { get; }
        public abstract MapZonesObserver mapZonesObserver { get; }

        public void BindTemplate(IMapZoneTemplate template)
        {
            _template = template;
        }

        protected void AddToObserver()
        {
            mapZonesObserver.AddToObserve(this);
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

