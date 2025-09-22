using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.UI
{
    public class GridContextButon : MonoBehaviour, IPointerClickHandler, IContextActionButton
    {
        [SerializeField] TextMeshProUGUI _buttonText;
        [SerializeField] Image _buttonBorder;
        [SerializeField] Image _buttonBackground;
        [SerializeField] Color _inactiveColor = Color.gray;
        [SerializeField] Color _buttonColor = Color.yellow;

        public event UnityAction<IContextAction> OnButtonActivation;

        IContextAction _buttonAction;

        public void ClearAction()
        {
            _buttonBorder.color = _inactiveColor;
            _buttonAction = null;
            _buttonText.text = "";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (_buttonAction is null) return;
            OnButtonActivation?.Invoke(_buttonAction);
        }

        public void BindAction(IContextAction action)
        {
            _buttonBorder.color = _buttonColor;
            _buttonAction = action;
            _buttonText.text = action.actionTitle;
        }


    }
}


