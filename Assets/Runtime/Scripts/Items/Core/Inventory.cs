using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Inventory : ScriptableObject, IInventory, IPermanentDependency
    {
        [SerializeField] ItemTemplate[] _testItems;
        [SerializeField] ResourceTemplate[] _startResources;
        [SerializeField] ItemSectionTemplate _storageTemplate;
        [SerializeField] ItemSectionTemplate _tempStorageTemplate;
        [SerializeField] ItemSectionTemplate[] _sectionsOrder;

        [SerializeField] Injector _selfInjector;

        ItemSection _storage;

        public StoredResources resources { get; private set; }

        List<IItemSection> _sections;
        Dictionary<IItemSectionTemplate, ItemSection> _sectionsByTemplate;

        bool _isInit;

        void OnEnable()
        {
            if (resources is not null) return;

            CreateSections();

            foreach (var template in _testItems)
            {
                AddItems(template.CreateItem(), template.maxStackSize);
            }

            _selfInjector.SetDependency(this);
        }

        public void AddItem(IItem item)
        {
            foreach (var section in _sections)
            {
                if (section.ItemMeet(item))
                {
                    section.AddItem(item);
                    break;
                }
            }
        }

        public void AddItems(IEnumerable<ItemSlotData> itemSlots)
        {
            foreach (var itemSlot in itemSlots)
            {
                AddItems(itemSlot.item, itemSlot.count);
            }
        }

        public void AddItems(IItem item, int count)
        {
            foreach (var section in _sections)
            {
                if (section.ItemMeet(item))
                {
                    section.AddItems(item, count);
                    break;
                }
            }
        }

        public int FindItemCount(IItem item)
        {
            var count = 0;

            foreach (var section in _sections)
            {
                if (section == _storage) continue;
                count += section.FindItemCount(item);
            }

            return count;
        }

        public ItemSection GetSection(IItemSectionTemplate template)
        {
            return _sectionsByTemplate[template];
        }

        public void RemoveOneItem(IItem item)
        {
            foreach(var section in _sections)
            {
                if (!section.HasItem(item)) continue;
                section.RemoveItem(item);
                break;
            }
        }

        private void CreateSections()
        {
            _sections = new List<IItemSection>(_sectionsOrder.Length + 1);
            _sectionsByTemplate = new(_sectionsOrder.Length);

            resources = new StoredResources(_startResources);
            _sections.Add(resources);

            foreach (var template in _sectionsOrder)
            {
                var section = new ItemSection(template);
                _sectionsByTemplate[template] = section;
                _sections.Add(section);
            }

            _storage = new ItemSection(_storageTemplate);
            _sectionsByTemplate[_storageTemplate] = _storage;
        }

        public void ClearTempStorage()
        {
            var tempStorage = GetSection(_tempStorageTemplate);
            if (tempStorage is null || tempStorage.isEmpty) return;
            tempStorage.ForEach(item => _storage.AddItems(item));
            tempStorage.Clear();
        }

        [ContextMenu("Clear")]
        void Clear()
        {
            _sections.ForEach(section => section.Clear());
        }

        void IPermanentDependency.ClearState()
        {

        }
    }
}