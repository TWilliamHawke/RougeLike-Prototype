using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Items
{
    public class Inventory : ScriptableObject
    {
        [SerializeField] Item[] _testItems;


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

        List<IItemSection> _sections;

        List<Item> _equipment;


        public void Init()
        {
            CreateSections();

            foreach (var item in _testItems)
            {
                AddItems(item, item.maxStackCount);
            }
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
            _sections = new List<IItemSection>(5);

            _potionsBag = new ItemSection<Potion>(3);
            _spellStrings = new ItemSection<SpellString>(-1);
            _scrollsBag = new ItemSection<MagicScroll>(5);
            _main = new ItemSection<Item>(12);
            _storage = new ItemSection<Item>(-1);

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
    }
}