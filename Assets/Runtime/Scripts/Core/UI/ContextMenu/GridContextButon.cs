using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.UI
{
    public class GridContextButon : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] TextMeshProUGUI _buttonText;
        [SerializeField] Image _buttonBorder;
        [SerializeField] Image _buttonBackground;
        [SerializeField] Color _inactiveColor = Color.gray;
        [SerializeField] Color _buttonColor = Color.yellow;

        IContextAction _buttonAction;

        public void ClearAction()
        {
            _buttonBorder.color = _inactiveColor;
            _buttonAction = null;
            _buttonText.text = "";
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _buttonAction?.DoAction();
        }

        public void BindAction(IContextAction action)
        {
            _buttonBorder.color = _buttonColor;
            _buttonAction = action;
            _buttonText.text = action.actionTitle;
        }


    }
}


