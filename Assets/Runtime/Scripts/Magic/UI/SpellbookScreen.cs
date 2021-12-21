using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;

namespace Magic.UI
{
    public class SpellbookScreen : UIPanelWithGrid<KnownSpellData>, IUIScreen
    {
        [SerializeField] Spellbook _spellBook;
        [SerializeField] Inventory _inventory;
        [SerializeField] Spell[] _testSpells;

        protected override List<KnownSpellData> _layoutElementsData => _spellBook.knownSpells;

        public void Init()
        {
            _spellBook.OnSpellPageOpen += OpenSpellPage;
            foreach (var spell in _testSpells)
            {
                _spellBook.AddSpell(spell);
                _spellBook.AddSpell(spell);
                _spellBook.AddSpell(spell);
            }
        }

        private void OnDestroy()
        {
            _spellBook.Clear();
            _spellBook.OnSpellPageOpen -= OpenSpellPage;
        }

        private void OnEnable()
        {
            UpdateLayout();
        }

        void OpenSpellPage(KnownSpellData data)
        {
            Debug.Log("open page");
        }


    }
}