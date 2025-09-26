using System.Collections.Generic;
using Core;
using Core.UI;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "ItemSectionTemplate", menuName = "Items/ItemSectionTemplate")]
    public class ItemSectionTemplate : ScriptableObject, IItemSectionTemplate, IContextActionSource
    {
        [SerializeField] LocalString _sectionName;
        [SerializeField] ItemStorageType _storageType;
        [SerializeField] int _startCapacity;
        [SerializeField] bool _hideifEmpty;
        [SerializeField] bool _allItemsAreMeet;
        [SerializeField] ContextActionList _actions;
        [HideIf("_allItemsAreMeet", true)]
        [SerializeField] ItemType _itemType;

        public int startCapacity => _startCapacity;
        public ItemStorageType storageType => _storageType;
        public string sectionName => _sectionName;
        public bool hideifEmpty => _hideifEmpty;

        public IEnumerable<ContextActionTemplate> GetActions()
        {
            return _actions;
        }

        public bool ItemTypeIsMeet(IItem someItem)
        {
            return _allItemsAreMeet || _itemType == someItem.itemType;
        }
    }

}