using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "ItemSectionTemplate", menuName = "Items/ItemSectionTemplate")]
    public class ItemSectionTemplate : ScriptableObject, IItemSectionTemplate
    {
        [SerializeField] LocalString _sectionName;
        [SerializeField] ItemStorageType _storageType;
        [SerializeField] int _startCapacity;
        [SerializeField] bool _allItemsAreMeet;
        [HideIf("_allItemsAreMeet", true)]
        [SerializeField] ItemType _itemType;

        public int startCapacity => _startCapacity;
        public ItemStorageType storageType => _storageType;
        public string sectionName => _sectionName;

        public bool ItemTypeIsMeet(Item someItem)
        {
            return _allItemsAreMeet || _itemType == someItem.itemType;
        }
    }

}