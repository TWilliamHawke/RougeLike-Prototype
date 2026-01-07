using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core;
using Core.UI;

namespace Items
{
    public abstract class ItemTemplate : ScriptableObject, IContextActionSource
    {
        [SpritePreview]
        [SerializeField] Sprite _icon;
        [SerializeField] ItemType _itemType;
        [SerializeField] ItemSoundKit _soundKit;

        public Sprite icon => _icon;
        public ItemType itemType => _itemType;
        public int maxStackSize => _itemType.maxStackSize;
        public AudioClip useSound => _soundKit.useSound;
        public AudioClip dragSound => _soundKit.dragSound;

        public abstract IItem CreateItem(int rarity = 0);

        public IEnumerable<ContextActionTemplate> GetActions()
        {
            return _itemType.GetActions();
        }

        public bool HasItemType(ItemType itemType)
        {
            return _itemType == itemType;
        }
    }
}