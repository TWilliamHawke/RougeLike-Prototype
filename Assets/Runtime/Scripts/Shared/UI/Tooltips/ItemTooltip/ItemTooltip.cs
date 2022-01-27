using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI.Tooltips
{
    public class ItemTooltip : MonoBehaviour, ITooltip
    {
        [SerializeField] int _defaultTitleSize = 36;
		[SerializeField] Image _icon;
		[SerializeField] TextMeshProUGUI _title;
		[SerializeField] TextMeshProUGUI _itemType;
		[SerializeField] TextMeshProUGUI _description;
		[SerializeField] RectTransform _rectTransform;
        [SerializeField] VerticalLayoutGroup _layout;

        Dictionary<int, int> _titleFontSizes = new Dictionary<int, int>
        {
            {50, 18}, {40, 22}, {30, 24}, {20, 28},
        };

		public void SetTooltipData(ItemTooltipData tooltipData)
        {
            _icon.sprite = tooltipData.icon;
            _title.text = tooltipData.title;
            _itemType.text = tooltipData.itemType;
            _description.text = tooltipData.description;

            SetTitleFontSize(tooltipData.title);

        }

        void SetTitleFontSize(string titleText)
        {
            int fontSize = _defaultTitleSize;
            foreach (var pair in _titleFontSizes)
            {
                if (titleText.Length > pair.Key)
                {
                    fontSize = pair.Value;
                    break;
                }
            }
            _title.fontSize = fontSize;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
			UpdatePosition();
            gameObject.SetActive(true);
            _layout.enabled = false;
            LayoutRebuilder.ForceRebuildLayoutImmediate(_rectTransform);
            _layout.enabled = true;
        }

        public void UpdatePosition()
        {
            _rectTransform.position = UIHelpers.NormalizePosition(_rectTransform);
        }
    }
}