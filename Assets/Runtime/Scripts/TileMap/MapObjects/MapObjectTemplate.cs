using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Objects
{
    public abstract class MapObjectTemplate : ScriptableObject
    {
        [UseFileName]
        [SerializeField] string _displayName;
        [SpritePreview]
        [SerializeField] Sprite _icon;
		[TextArea(3,5)]
		[SerializeField] string _interactionDescription;

    }
}

