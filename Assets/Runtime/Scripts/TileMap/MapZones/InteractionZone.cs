using System.Collections;
using System.Collections.Generic;
using Entities.NPC;
using Entities.PlayerScripts;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Zones
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class InteractionZone : MonoBehaviour
    {
        IMapZone _zoneLogic;
        public event UnityAction OnPlayerEnter;
        public event UnityAction OnPlayerExit;

        void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.GetComponent<Player>()) return;
            OnPlayerEnter?.Invoke();
        }

        void OnTriggerExit2D(Collider2D other)
        {
            if (!other.GetComponent<Player>()) return;
            OnPlayerExit?.Invoke();
        }

        void OnDisable()
        {
            if (_zoneLogic == null) return;
            _zoneLogic.RemoveFromObserve(this);
        }

        public void Init(IMapZone zoneLogic)
        {
            gameObject.SetActive(true);
            _zoneLogic = zoneLogic;
            _zoneLogic.AddToObserve(this);
        }

        public void Resize(Vector2Int size)
        {
            GetComponent<BoxCollider2D>().size = size;
        }
    }
}


