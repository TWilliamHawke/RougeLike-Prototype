using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Core.UI
{
    public class ModalWindow : MonoBehaviour, IModalWindow
    {
        [Header("UI Elements")]
        [SerializeField] TextMeshProUGUI _title;
        [SerializeField] TextMeshProUGUI _mainText;
        [SerializeField] Image _mainImage;
        [SerializeField] VerticalLayoutGroup _resourcePanel;

        [SerializeField] Button _cancelButton;

        IContextAction _action;

        void IModalWindow.Open(ModalWindowData data)
        {
            _title.text = data.title;
            _mainText.text = data.mainText;
            _mainImage.sprite = data.mainImage;
            _action = data.action;
        }

        //event handler in editor
        public void ConfirmHandler()
        {
            _action.DoAction();
        }

        //event handler in editor
        public void CancelHandler()
        {

        }
    }
}


