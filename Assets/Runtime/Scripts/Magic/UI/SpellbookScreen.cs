using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.UI;

namespace Magic.UI
{
    public class SpellbookScreen : UIPanelWithGrid<KnownSpellData>, IUIScreen
    {
        [SerializeField] Spellbook _spellBook;
        [SerializeField] Inventory _inventory;
        [SerializeField] Spell[] _testSpells;
        [Header("UI Elements")]
        [SerializeField] SpellPage _spellPage;
        [SerializeField] GridLayoutGroup _spellGrid;

        protected override IEnumerable<KnownSpellData> _layoutElementsData => _spellBook.knownSpells;

        public void Init()
        {
            _spellBook.OnSpellPageOpen += OpenSpellPage;
            _spellPage.Init();

            foreach (var spell in _testSpells)
            {
                _spellBook.AddSpell(spell);
                _spellBook.AddSpell(spell);
                _spellBook.AddSpell(spell);
            }
        }

        void OnDestroy()
        {
            _spellBook.Clear(); //only for tests
            _spellBook.OnSpellPageOpen -= OpenSpellPage;
        }

        void OnEnable()
        {
            CloseSpellPage();
            UpdateLayout();
        }

        //use as button listener
        public void CloseSpellPage()
        {
            _spellGrid.gameObject.SetActive(true);
            _spellPage.gameObject.SetActive(false);
        }

        void OpenSpellPage(KnownSpellData data)
        {
            _spellGrid.gameObject.SetActive(false);
            _spellPage.Open(data);
        }




    }
}