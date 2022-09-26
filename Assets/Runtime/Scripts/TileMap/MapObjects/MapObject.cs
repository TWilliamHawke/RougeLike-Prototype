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
        public event UnityAction<MapObjectTask> OnTaskChange;

        public abstract MapObjectTemplate template { get; }
        public abstract MapObjectTask task { get; }

        public abstract IMapActionsController actionsController { get; }

        protected void InvokeTaskEvent()
        {
            OnTaskChange?.Invoke(task);
        }

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

