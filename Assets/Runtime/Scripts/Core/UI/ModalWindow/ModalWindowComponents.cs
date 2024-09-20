using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Items;

namespace Core.UI
{
    public class ModalWindowComponents : UIPanelWithGrid<ItemSlotData>
    {
        [Header("UI Elements")]
        [SerializeField] TextMeshProUGUI _title;
        [SerializeField] TextMeshProUGUI _mainText;
        [SerializeField] RectTransform _imageWrapper;
        [SerializeField] Image _mainImage;

        [SerializeField] Button _cancelButton;
        [SerializeField] TextMeshProUGUI _confirmButtonText;

        const string CONFIRM_BUTTON_TITLE = "Confirm";

        IEnumerable<ItemSlotData> _resourcesData;

        protected override IEnumerable<ItemSlotData> _layoutElementsData => _resourcesData;

        public void ResetAll()
        {
            _mainText.Hide();
            _imageWrapper.gameObject.SetActive(false);
            _confirmButtonText.text = CONFIRM_BUTTON_TITLE;
            _cancelButton.Hide();
            SetLayoutVisibility(false);
        }

        public void ChangeTitle(string title)
        {
            _title.text = title;
        }

        public void ShowText(string text)
        {
            _mainText.text = text;
            _mainText.Show();
        }

        public void ShowImage(Sprite sprite)
        {
            _mainImage.sprite = sprite;
            _imageWrapper.gameObject.SetActive(true);
        }

        public void ChangeConfirmButtonText(string text)
        {
            _confirmButtonText.text = text;
        }

        public void ShowCloseButton()
        {
            _cancelButton.Show();
        }

        public void ShowResources(IEnumerable<ItemSlotData> resources)
        {
            _resourcesData = resources;
            UpdateLayout(resources);
            SetLayoutVisibility(true);
        }
    }
}


