using UnityEngine;
using Magic;
using Core.UI;

namespace Items
{
    public class SpellLearningWindowController: MonoBehaviour
    {
        [SerializeField] Spellbook _spellBook;
        [InjectField] ModalWindowController _modalWindow;
        [InjectField] UIScreen _spellBookScreen;

        [SerializeField] string _successTitle;
        [SerializeField] string _failureTitle;
        [SerializeField] string _failureMainText;

        private void Awake()
        {
            _spellBook.OnSpellAdded += ShowSuccessWindow;
            _spellBook.OnSpellAddedFailure += ShowFailureWindow;
        }

        private void OnDestroy()
        {
            _spellBook.OnSpellAdded -= ShowSuccessWindow;
            _spellBook.OnSpellAddedFailure -= ShowFailureWindow;
        }


        public void ShowFailureWindow()
        {
            var modalWindowData = new ModalWindowData(_failureTitle, _failureMainText);
            _modalWindow.OpenWindow(modalWindowData);
        }

        public void ShowSuccessWindow(Spell spell)
        {
            var modalWindowData = new ModalWindowData
            {
                title = _successTitle,
                mainText = spell.displayName,
                mainImage = spell.icon,
                action = new OpenScreen(_spellBookScreen, "Open Spellbook")
            };

            _modalWindow.OpenWindow(modalWindowData);
        }
    }
}


