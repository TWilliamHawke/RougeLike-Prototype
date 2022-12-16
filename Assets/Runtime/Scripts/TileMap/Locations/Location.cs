using System.Collections;
using System.Collections.Generic;
using Map.Objects;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Map.Locations
{
    public abstract class Location : ScriptableObject, IMapObject, IMapActionList
    {
        [UseFileName]
        [SerializeField] string _displayName;
        [SpritePreview]
        [SerializeField] Sprite _icon;

        public abstract TaskData currentTask { get; protected set; }
        public abstract LocationMapData Create(Tilemap tilemap);

        public string displayName => _displayName;
        public Sprite icon => _icon;

        public IMapActionList mapActionList => this;
        public IMapAction this[int idx] => _actions[idx];
        public int count => _actions.Count;
		
		List <IMapAction> _actions = new();
    }
}

