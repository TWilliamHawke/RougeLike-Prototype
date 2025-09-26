using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Core.UI;

namespace Items
{
    public abstract class ItemTemplate : ScriptableObject, IContextActionSource, IItemTemplate
    {
        [UseFileName]
        [SerializeField] string _displayName;
        [SpritePreview]
        [SerializeField] Sprite _icon;
        [SerializeField] ItemType _itemType;
        [SerializeField] int _value;
        [SerializeField] ItemSoundKit _soundKit;

        public virtual string displayName => _displayName;
        public Sprite icon => _icon;
        public ItemType itemType => _itemType;
        public int maxStackSize => _itemType.maxStackSize;
        public virtual int value => _value;
        public AudioClip useSound => _soundKit.useSound;
        public AudioClip dragSound => _soundKit.dragSound;

        public abstract string GetDescription();
        public abstract IItem CreateItem(int rarity = 0);

        public IEnumerable<ContextActionTemplate> GetActions()
        {
            return _itemType.actions;
        }

        public bool HasItemType(ItemType itemType)
        {
            return _itemType == itemType;
        }
    }
}