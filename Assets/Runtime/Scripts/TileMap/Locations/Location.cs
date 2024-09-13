using System.Collections;
using System.Collections.Generic;
using Map.Zones;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map.Locations
{
    public abstract class Location : ScriptableObject, IMapZone, IMapActionList
    {
        [UseFileName]
        [SerializeField] string _displayName;
        [SpritePreview]
        [SerializeField] Sprite _icon;

        public abstract TaskData currentTask { get; protected set; }
        public abstract LocationMapData Create(Tilemap tilemap);

        public string displayName => _displayName;
        public Sprite icon => _icon;

        public IMapActionList actionList => this;
        public IMapAction this[int idx] => _actions[idx];
        public int count => _actions.Count;
		
		List <IMapAction> _actions = new();

        public void AddToObserve(InteractionZone target)
        {

        }

        public void RemoveFromObserve(InteractionZone target)
        {

        }


    }
}

