using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Objects
{
    public abstract class MapObjectAction : ScriptableObject, IActionData
    {
        [UseFileName]
        [SerializeField] string _displayName;
        [SpritePreview]
        [SerializeField] Sprite _icon;

		public abstract IMapActionLogic actionLogic { get; }

        public string displayName => _displayName;
        public Sprite icon => _icon;
    }

    public interface IActionData
    {
        string displayName { get; }
        Sprite icon { get; }
    }

}

