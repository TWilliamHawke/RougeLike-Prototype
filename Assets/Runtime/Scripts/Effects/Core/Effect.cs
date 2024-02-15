using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Effects
{
    public class Effect : ScriptableObject
    {
        [UseFileName]
        [SerializeField] string _displayName;
        [Multiline(4)]
        [SerializeField] string _description;
        [SpritePreview]
        [SerializeField] Sprite _icon;

        public Sprite icon => _icon;
        public string description => _description;

        public virtual bool CanApply(IEffectSource source, IEffectTarget target)
        {
            return true;
        }

    }
}