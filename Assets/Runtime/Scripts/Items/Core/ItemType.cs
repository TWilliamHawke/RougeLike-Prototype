using Core;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(menuName = "Items/Item Type", fileName = "Item Type")]
    public class ItemType : ScriptableObject
    {
        [SerializeField] LocalString _displayName;
        [SpritePreview]
        [SerializeField] Sprite _icon;
        [SerializeField] ContextActionList _actions;

        public string displayName => _displayName;
        public Sprite icon => _icon;
        public ContextActionList actions => _actions;
    }
}