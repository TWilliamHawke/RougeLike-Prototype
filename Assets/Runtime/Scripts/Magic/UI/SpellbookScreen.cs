using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.UI;
using UI.DragAndDrop;
using TMPro;

namespace Magic.UI
{
    public class SpellbookScreen : MonoBehaviour
    {
        [SerializeField] int _spellsPerPage = 6;
        [SerializeField] Spellbook _spellBook;
        [SerializeField] Inventory _inventory;
        [SerializeField] Injector _spellBookScreenInjector;
        [Header("UI Elements")]
        [SerializeField] UIScreen _spellbookCanvas;
        [SerializeField] SpellList _spellList;
        [SerializeField] SpellPage _spellPage;
        [SerializeField] ResourcesPage _resourcesPage;
        [SerializeField] TextMeshProUGUI _pageNumber;
        [SerializeField] Spell[] _testSpells;

        int _maxPage => _spellBook.totalCount / _spellsPerPage + 1;

        int _currentPage = 1;

        private void Awake()
        {
            _spellBook.OnSpellPageOpen += OpenSpellPage;
            _spellBook.OnSpellSelect += Close;
            _spellbookCanvas.OnScreenOpen += PrepareBook;

            _spellBookScreenInjector.SetDependency(_spellbookCanvas);

            _spellPage.Init();
            _resourcesPage.Init();

            _spellBook.Clear(); //only for tests
            foreach (var spell in _testSpells)
            {
                _spellBook.AddSpellCopy(spell);
                _spellBook.AddSpellCopy(spell);
                _spellBook.AddSpellCopy(spell);
            }
        }

        private void OnDestroy()
        {
            _spellBook.OnSpellPageOpen -= OpenSpellPage;
            _spellBook.OnSpellSelect -= Close;
            _spellbookCanvas.OnScreenOpen -= PrepareBook;
        }

        void PrepareBook()
        {
            CloseSpellPage();
            _spellList.UpdateLayout(FindSpellsOnPage());
        }

        //use as unityEvent
        public void CloseSpellPage()
        {
            _spellList.Show();
            _spellPage.Hide();
            _resourcesPage.Hide();
        }

        private void UpdatePageText()
        {
            _pageNumber.text = $"Page {_currentPage}/{_maxPage}";
        }

        private IEnumerable<KnownSpellData> FindSpellsOnPage()
        {
            int maxIdx = Mathf.Min(_currentPage * _spellsPerPage, _spellBook.totalCount);
            int startIdx = (_currentPage - 1) * _spellsPerPage;

            for (int i = startIdx; i < maxIdx; i++)
            {
                yield return _spellBook[i];
            }
        }



        void OpenSpellPage(KnownSpellData data)
        {
            _spellList.Hide();
            _spellPage.Open(data);
            _resourcesPage.Show();
        }

        void Close(KnownSpellData _)
        {
            _spellbookCanvas.Close();
        }




    }
}