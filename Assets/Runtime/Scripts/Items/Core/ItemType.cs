using System.Collections;
using System.Collections.Generic;
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
        [SerializeField] int _maxStackSize = 1;
        [SerializeField] ContextActionList _actions;

        public string displayName => _displayName;
        public Sprite icon => _icon;
        public int maxStackSize => _maxStackSize;

        public IEnumerable<ContextActionTemplate> GetActions()
        {
            return _actions.GetElements();
        }
    }
}