using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Objects
{
    public abstract class MapObjectAction : ScriptableObject
    {
        [UseFileName]
        [SerializeField] string _displayName;
        [SpritePreview]
        [SerializeField] Sprite _icon;

        public abstract void DoAction();
		public abstract IMapActionLogic actionLogic { get; }
    }

    public interface IMapActionLogic
    {

    }

}

