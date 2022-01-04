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
        [SerializeField] ResourcesPage _resourcesPage;
        [SerializeField] GridLayoutGroup _spellGrid;

        protected override IEnumerable<KnownSpellData> _layoutElementsData => _spellBook.knownSpells;

        public void Init()
        {
            _spellBook.OnSpellPageOpen += OpenSpellPage;
            _spellBook.OnSpellSelect += Close;
            _spellPage.Init();
            _resourcesPage.Init();

            _spellBook.Clear(); //only for tests
            foreach (var spell in _testSpells)
            {
                _spellBook.AddSpell(spell);
                _spellBook.AddSpell(spell);
                _spellBook.AddSpell(spell);
            }
        }

        void OnDestroy()
        {
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

        void Close(KnownSpellData _)
        {
            gameObject.SetActive(false);
        }




    }
}