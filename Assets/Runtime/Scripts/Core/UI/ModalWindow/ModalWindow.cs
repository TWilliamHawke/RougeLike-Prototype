using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Items;

namespace Core.UI
{
    public class ModalWindow : UIPanelWithGrid<ItemSlotData>, IModalWindow
    {
        [Header("UI Elements")]
        [SerializeField] TextMeshProUGUI _title;
        [SerializeField] TextMeshProUGUI _mainText;
        [SerializeField] Image _mainImage;
        [SerializeField] VerticalLayoutGroup _resourcePanel;

        [SerializeField] Button _cancelButton;
        [SerializeField] TextMeshProUGUI _confirmButtonText;

        const string CONFIRM_BUTTON_TITLE = "Confirm";

        IContextAction _action;
        IEnumerable<ItemSlotData> _resourcesData;

        protected override IEnumerable<ItemSlotData> _layoutElementsData => _resourcesData;

        private void OnEnable()
        {
            UpdateLayout();
        }

        public void Open(ModalWindowData data)
        {
            _title.text = data.title;
            _mainText.text = data.mainText;
            _mainImage.gameObject.SetActive(data.mainImage is not null);
            _mainImage.sprite = data.mainImage;
            _action = data.action;
            _confirmButtonText.text = data.action?.actionTitle ?? CONFIRM_BUTTON_TITLE;
            _resourcesData = data.resourcesData ?? new ItemSection<Item>(ItemStorageType.none);
            gameObject.SetActive(true);
        }

        //event handler in editor
        public void ConfirmHandler()
        {
            _action?.DoAction();
            gameObject.SetActive(false);
        }

        //event handler in editor
        public void CancelHandler()
        {
            gameObject.SetActive(false);
        }
    }
}


