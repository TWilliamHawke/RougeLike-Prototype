using System.Collections;
using System.Collections.Generic;
using Entities.PlayerScripts;
using UnityEngine;
using UnityEngine.Events;

namespace Map.Objects
{
    public abstract class MapObject : MonoBehaviour
    {
		public event UnityAction<MapObject> OnPlayerEnter;
		public event UnityAction<MapObject> OnPlayerExit;
        public event UnityAction<MapObjectTaskData> OnTaskChange;

        public abstract MapObjectTemplate template { get; }
        public abstract MapObjectTaskData task { get; }

        public abstract IMapActionsController actionsController { get; }

        protected void UpdateTask()
        {
            OnTaskChange?.Invoke(task);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
			if(!other.GetComponent<Player>()) return;
			OnPlayerEnter?.Invoke(this);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
			if(!other.GetComponent<Player>()) return;
			OnPlayerExit?.Invoke(this);
        }


    }
}

