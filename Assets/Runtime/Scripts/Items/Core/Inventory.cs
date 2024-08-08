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

        ItemSection<Item> _storage;

        public ItemSection<Potion> potionsBag { get; private set; }
        public ItemSection<MagicScroll> scrollsBag  { get; private set; }
        public ItemSection<Item> main  { get; private set; }
        public ItemSection<Item> tempStorage  { get; private set; }
        public ItemSection<SpellString>  spellStrings  { get; private set; }
        public StoredResources resources  { get; private set; }
        public IEnumerable<Item> equipment  => _equipment;

        List<IItemSection> _sections;

        List<Item> _equipment;

        bool _isInit;

        private void OnEnable()
        {
            if(resources is not null) return;

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

            resources = new StoredResources(_startResources);
            potionsBag = new ItemSection<Potion>(ItemStorageType.inventory, 3);
            spellStrings = new ItemSection<SpellString>(ItemStorageType.inventory);
            scrollsBag = new ItemSection<MagicScroll>(ItemStorageType.inventory, 5);
            main = new ItemSection<Item>(ItemStorageType.inventory, 12);
            tempStorage = new ItemSection<Item>(ItemStorageType.storage);

            _storage = new ItemSection<Item>(ItemStorageType.storage);

            _sections.Add(resources);
            _sections.Add(potionsBag);
            _sections.Add(scrollsBag);
            _sections.Add(spellStrings);
            //if special sections is full - add to main
            _sections.Add(main);
            //if main is full - add to temporary storage
            _sections.Add(tempStorage);
        }

        public void ClearTempStorage()
        {
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