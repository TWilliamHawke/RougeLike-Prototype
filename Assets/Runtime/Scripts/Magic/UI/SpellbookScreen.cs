using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.UI;
using UI.DragAndDrop;
using TMPro;

namespace Magic.UI
{
    public class SpellbookScreen : MonoBehaviour, IObserver<KnownSpellSlot>
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

        private void Awake()
        {
            _spellList.AddObserver(this);
            _spellBook.OnUpdate += UpdatePage;
            _spellbookCanvas.OnScreenOpen += PrepareBook;

            _spellBookScreenInjector.SetDependency(_spellbookCanvas);

            _nextButton.onClick.AddListener(ShowNextPage);
            _prevButton.onClick.AddListener(ShowPrevPage);

            _spellBook.Clear(); //only for tests
            foreach (var spell in _testSpells)
            {
                _spellBook.TryAddSpell(spell);
            }
        }

        private void OnDestroy()
        {
            _spellbookCanvas.OnScreenOpen -= PrepareBook;
            _spellBook.OnUpdate -= UpdatePage;
        }

        void PrepareBook()
        {
            UpdatePage();
            _spellList.UpdateLayout(FindSpellsOnPage());
        }

        //use as unityEvent
        public void CloseSpellPage()
        {
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

        private void CloseScreen(KnownSpellData _)
        {
            _spellbookCanvas.Close();
        }

        void IObserver<KnownSpellSlot>.AddToObserve(KnownSpellSlot target)
        {
            target.OnSpellSelect += CloseScreen;
            target.OnEditButtonClick += CloseScreen;
        }

        void IObserver<KnownSpellSlot>.RemoveFromObserve(KnownSpellSlot target)
        {
            target.OnSpellSelect -= CloseScreen;
            target.OnEditButtonClick -= CloseScreen;
        }
    }
}