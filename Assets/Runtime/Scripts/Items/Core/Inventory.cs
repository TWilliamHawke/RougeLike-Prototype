using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    public class Inventory : ScriptableObject, IPermanentDependency
    {
        [SerializeField] Item[] _testItems;
        [SerializeField] Resource[] _startResources;

        [SerializeField] Injector _selfInjector;

        StoredResources _resources;
        ItemSection<Potion> _potionsBag;
        ItemSection<MagicScroll> _scrollsBag;
        ItemSection<Item> _main;
        ItemSection<Item> _storage;
        ItemSection<SpellString> _spellStrings;

        public ItemSection<Potion> potionsBag => _potionsBag;
        public ItemSection<MagicScroll> scrollsBag => _scrollsBag;
        public ItemSection<Item> main => _main;
        public ItemSection<SpellString>  spellStrings => _spellStrings;
        public IEnumerable<Item> equipment => _equipment;
        public StoredResources resources => _resources;

        List<IItemSection> _sections;

        List<Item> _equipment;

        bool _isInit;

        private void OnEnable()
        {
            if(_resources is not null) return;

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
            foreach(var itemSlot in itemSlots)
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
                if(section == _storage) continue;
                count += section.FindItemCount(item);
            }

            return count;
        }


        private void CreateSections()
        {
            _sections = new List<IItemSection>(6);

            _resources = new StoredResources(_startResources);
            _potionsBag = new ItemSection<Potion>(ItemStorageType.inventory, 3);
            _spellStrings = new ItemSection<SpellString>(ItemStorageType.inventory);
            _scrollsBag = new ItemSection<MagicScroll>(ItemStorageType.inventory, 5);
            _main = new ItemSection<Item>(ItemStorageType.inventory, 12);
            _storage = new ItemSection<Item>(ItemStorageType.storage);

            _sections.Add(_resources);
            _sections.Add(_potionsBag);
            _sections.Add(_scrollsBag);
            _sections.Add(_spellStrings);
            //if special sections is full - add to main
            _sections.Add(_main);
            //if main is full - add to storage
            _sections.Add(_storage);
        }


        [ContextMenu("Clear")]
        void Clear()
        {
            foreach (var section in _sections)
            {
                section.Clear();
            }
        }

        public void ClearState()
        {

        }
    }
}