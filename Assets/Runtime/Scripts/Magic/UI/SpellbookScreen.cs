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
        }

        void OnEnable()
        {
            CloseSpellPage();
            _spellList.UpdateSpellList();
        }

        public void ShowBackGround()
        {
            _background.gameObject.SetActive(true);
        }

        public void HideBackGround()
        {
            _background.gameObject.SetActive(false);
        }

        //use as unityEvent
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




    }
}