using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace UI.Tooltips
{
    public class SimpleTooltip : MonoBehaviour, ITooltip
    {
        [SerializeField] TextMeshProUGUI _toottipText;
        [SerializeField] RectTransform _rectTransform;

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void SetTooltipText(string tooltipText)
        {
            _toottipText.text = tooltipText;

        }

        public void Show()
        {
			UpdatePosition();
            gameObject.SetActive(true);
        }

        public void UpdatePosition()
        {
            _rectTransform.position = UIHelpers.NormalizePosition(_rectTransform);
        }
    }
}