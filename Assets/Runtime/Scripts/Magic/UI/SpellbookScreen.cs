using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Magic.UI
{
    public class SpellbookScreen : MonoBehaviour
    {
        [SerializeField] int _spellsPerPage = 6;
        [SerializeField] Spellbook _spellBook;
        [SerializeField] Injector _spellBookScreenInjector;
        [Header("UI Elements")]
        [SerializeField] UIScreen _spellbookCanvas;
        [SerializeField] SpellList _spellList;
        [SerializeField] TextMeshProUGUI _pageNumber;
        [SerializeField] Spell[] _testSpells;
        [SerializeField] Button _prevButton;
        [SerializeField] Button _nextButton;

        int _maxPage => Mathf.CeilToInt(_spellBook.totalCount / (float)_spellsPerPage);

        int _currentPage = 1;

        void Awake()
        {
            _spellBook.OnUpdate += UpdatePage;
            _spellbookCanvas.OnScreenOpen += PrepareBook;

            _spellBookScreenInjector.SetDependency(_spellbookCanvas);

            _nextButton.onClick.AddListener(ShowNextPage);
            _prevButton.onClick.AddListener(ShowPrevPage);

            foreach (var spell in _testSpells)
            {
                _spellBook.TryAddSpell(spell);
            }
        }

        void OnDestroy()
        {
            _spellBook.OnUpdate -= UpdatePage;
            _spellBook.Clear(); //only for tests
        }

        public void CloseScreen()
        {
            _spellbookCanvas.Close();
        }

        //use as unityEvent
        public void CloseSpellPage()
        {
        }

        private void PrepareBook()
        {
            UpdatePage();
            _spellList.UpdateLayout(FindSpellsOnPage());
        }

        private void ShowPrevPage()
        {
            _currentPage--;
            UpdatePage();
        }

        private void ShowNextPage()
        {
            _currentPage++;
            UpdatePage();
        }

        private void UpdatePage()
        {
            _spellList.UpdateLayout(FindSpellsOnPage());
            _pageNumber.text = $"Page {_currentPage}/{_maxPage}";
            _prevButton.interactable = _currentPage > 1;
            _nextButton.interactable = _currentPage < _maxPage;
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
    }
}