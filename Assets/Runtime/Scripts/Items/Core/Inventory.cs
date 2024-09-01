using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Inventory : ScriptableObject, IPermanentDependency
    {
        [SerializeField] Item[] _testItems;
        [SerializeField] Resource[] _startResources;
        [SerializeField] ItemSectionTemplate _storageTemplate;
        [SerializeField] ItemSectionTemplate _tempStorageTemplate;
        [SerializeField] ItemSectionTemplate[] _sectionsOrder;

        [SerializeField] Injector _selfInjector;

        ItemSection _storage;

        public StoredResources resources { get; private set; }
        public IEnumerable<Item> equipment => _equipment;

        List<IItemSection> _sections;
        Dictionary<IItemSectionTemplate, ItemSection> _sectionsByTemplate;

        List<Item> _equipment;

        bool _isInit;

        private void OnEnable()
        {
            if (resources is not null) return;

            CreateSections();

            foreach (var item in _testItems)
            {
                AddItems(item, item.maxStackSize);
            }

            _selfInjector.SetDependency(this);
        }

        public void AddItem(Item item)
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

        public Item GetEquipment(EquipmentTypes type)
        {
            return _equipment[(int)type];
        }

        public void AddEquipment(EquipmentTypes type, Item item)
        {
            _equipment[(int)type] = item;
        }

        public void AddItems(IEnumerable<ItemSlotData> itemSlots)
        {
            foreach (var itemSlot in itemSlots)
            {
                AddItems(itemSlot.item, itemSlot.count);
            }
        }

        public void AddItems(Item item, int count)
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

        public int FindItemCount(Item item)
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