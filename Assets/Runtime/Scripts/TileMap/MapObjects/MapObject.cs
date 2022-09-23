using System.Collections;
using System.Collections.Generic;
using Entities.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Objects
{
    public abstract class MapObject : MonoBehaviour
    {
		public event UnityAction<MapObject> OnPlayerEnter;
		public event UnityAction<MapObject> OnPlayerExit;
        public abstract MapObjectTemplate template { get; }

        private void OnTriggerEnter2D(Collider2D other)
        {
			if(!other.GetComponent<PlayerCore>()) return;
			OnPlayerEnter?.Invoke(this);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
			if(!other.GetComponent<PlayerCore>()) return;
			OnPlayerExit?.Invoke(this);
        }


    }
}

