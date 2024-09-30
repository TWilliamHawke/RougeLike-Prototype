using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class ScreenSelector : MonoBehaviour
    {
        [SerializeField] UIScreen _menu;
        [SerializeField] LayoutGroup _mainButtons;
        [SerializeField] LayoutGroup _characterButtons;
        [SerializeField] LayoutGroup _worldButtons;

        public void OpenScreen(UIScreen screen)
        {
            _menu.Close();
            screen.Open();
        }

        public void ShowMainButtons()
        {
            ShowButtons(_mainButtons);
        }

        public void ShowCharacterButtons()
        {
            ShowButtons(_characterButtons);
        }

        public void ShowWorldButtons()
        {
            ShowButtons(_worldButtons);
        }

        public void CloseMenu()
        {
            _menu.Close();
        }

        private void ShowButtons(LayoutGroup layout)
        {
            _mainButtons.Hide();
            _characterButtons.Hide();
            _worldButtons.Hide();
            layout.Show();
            _menu.Open();
        }
    }
}
