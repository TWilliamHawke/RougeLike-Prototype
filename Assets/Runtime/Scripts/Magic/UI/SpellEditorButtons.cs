using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Magic.UI
{
    public class SpellEditorButtons : MonoBehaviour
    {
        [SerializeField] Button _closeButton;
        [SerializeField] Button _clearSlotButon;
        [SerializeField] HorizontalLayoutGroup _buttonWrapper;
        [SerializeField] Image _dustIcon;
        [SerializeField] TextMeshProUGUI _rankUpCost;
        [SerializeField] TextMeshProUGUI _clearCost;

        public event UnityAction OnClearButtonClick;
        public event UnityAction OnCloseButtonClick;
        public event UnityAction OnConfirmButtonClick;

        public void HideAll()
        {
            _closeButton.Hide();
            _clearSlotButon.Hide();
            _buttonWrapper.Hide();
        }

        public void ShowRankUpButton(int cost)
        {
            _buttonWrapper.Show();
            _dustIcon.Show();
            _rankUpCost.Show();
            _rankUpCost.text = cost.ToString();
        }

        public void ShowConfirmButton()
        {
            _buttonWrapper.Show();
            _dustIcon.Hide();
            _rankUpCost.Hide();
        }

        public void ShowCloseButton()
        {
            _closeButton.Show();
        }

        public void ShowClearButton(int cost)
        {
            _clearSlotButon.Show();
            _clearCost.text = cost.ToString();
        }

        public void InvokeClearEvent()
        {
            OnClearButtonClick?.Invoke();
        }

        public void InvokeConfirmEvent()
        {
            OnConfirmButtonClick?.Invoke();
        }

        public void InvokeCloseEvent()
        {
            OnCloseButtonClick?.Invoke();
        }
    }
}
