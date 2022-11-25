using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.UI;
using UI.DragAndDrop;

namespace Magic.UI
{
    public class SpellbookScreen : MonoBehaviour, IUIScreen
    {
        [SerializeField] Spellbook _spellBook;
        [SerializeField] Inventory _inventory;
        [SerializeField] DragController _dragController;
        [Header("UI Elements")]
        [SerializeField] Image _background;
        [SerializeField] SpellList _spellList;
        [SerializeField] SpellPage _spellPage;
        [SerializeField] ResourcesPage _resourcesPage;
        [SerializeField] GridLayoutGroup _spellGrid;
        [SerializeField] Spell[] _testSpells;

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
            _spellList.UpdateSpellList();
        }

        //use as button listener
        public void CloseSpellPage()
        {
            _spellList.Show();
            _spellPage.Hide();
            _resourcesPage.Hide();
        }

        void OpenSpellPage(KnownSpellData data)
        {
            _spellList.Hide();
            _spellPage.Open(data);
            _resourcesPage.Show();
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