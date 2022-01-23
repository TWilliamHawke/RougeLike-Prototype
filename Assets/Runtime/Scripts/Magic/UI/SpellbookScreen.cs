using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.UI;
using UI.DragAndDrop;

namespace Magic.UI
{
    public class SpellbookScreen : UIPanelWithGrid<KnownSpellData>, IUIScreen
    {
        [SerializeField] Spellbook _spellBook;
        [SerializeField] Inventory _inventory;
        [SerializeField] DragController _dragController;
        [Header("UI Elements")]
        [SerializeField] Image _background;
        [SerializeField] SpellPage _spellPage;
        [SerializeField] ResourcesPage _resourcesPage;
        [SerializeField] GridLayoutGroup _spellGrid;
        [SerializeField] Spell[] _testSpells;

        protected override IEnumerable<KnownSpellData> _layoutElementsData => _spellBook.knownSpells;

        public void Init()
        {
            _spellBook.OnSpellPageOpen += OpenSpellPage;
            _spellBook.OnSpellSelect += Close;
            _dragController.OnBeginDrag += HideBackGround;
            _dragController.OnEndDrag += ShowBackGround;

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
            _spellBook.OnSpellSelect -= Close;
            _dragController.OnBeginDrag -= HideBackGround;
            _dragController.OnEndDrag -= ShowBackGround;
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
            _resourcesPage.gameObject.SetActive(false);
        }

        void OpenSpellPage(KnownSpellData data)
        {
            _spellGrid.gameObject.SetActive(false);
            _spellPage.Open(data);
            _resourcesPage.gameObject.SetActive(true);
        }

        void Close(KnownSpellData _)
        {
            gameObject.SetActive(false);
        }

        void ShowBackGround()
        {
            _background.gameObject.SetActive(true);
        }

        void HideBackGround(object _)
        {
            _background.gameObject.SetActive(false);
        }




    }
}