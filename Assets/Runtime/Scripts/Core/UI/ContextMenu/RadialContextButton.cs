using System.Collections;
using System.Collections.Generic;
using Items;
using TMPro;
using UI.DragAndDrop;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class RadialContextButton : MonoBehaviour, IDropTarget<ItemSlotData>
    {
        [SerializeField] RadialButtonPosition _buttonPosition;
        [SerializeField] TextMeshProUGUI _buttonText;
        [SerializeField] Image _mask;

        IContextAction _buttonAction;
        public bool checkImageAlpha => true;

        public RadialButtonPosition buttonPosition => _buttonPosition;

        public bool DataIsMeet(ItemSlotData data)
        {
            return _buttonPosition == RadialButtonPosition.middle || _buttonAction is not null;
        }

        public void DropData(ItemSlotData data)
        {
            _buttonAction?.DoAction();
        }

        public void Highlight()
        {
            _mask.gameObject.SetActive(true);
        }

        public void UnHighlight()
        {
            _mask.gameObject.SetActive(false);
        }

        public void BindAction(IContextAction action)
        {
            if(_buttonPosition == RadialButtonPosition.middle) return;
            _buttonAction = action;
            _buttonText.text = action.actionTitle;
        }

        public void ClearAction()
        {
            if(_buttonPosition == RadialButtonPosition.middle) return;
            _buttonAction = null;
            _buttonText.text = "";
        }
    }
}


