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

        public IItemSection potionsBag => _potionsBag;
        public IItemSection scrollsBag => _scrollsBag;
        public IItemSection main => _main;
        public ItemSection<SpellString>  spellStrings => _spellStrings;

        List<IItemSection> _sections;


        public void Init()
        {
            CreateSections();

            foreach (var item in _testItems)
            {
                AddItem(item);
                AddItem(item);
                AddItem(item);
                AddItem(item);
                AddItem(item);
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
            _potionsBag = new ItemSection<Potion>(3);
            _spellStrings = new ItemSection<SpellString>(-1);
            _scrollsBag = new ItemSection<MagicScroll>(5);
            _main = new ItemSection<Item>(12);
            _storage = new ItemSection<Item>(-1);
            _sections = new List<IItemSection>(5);

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