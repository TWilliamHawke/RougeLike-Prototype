using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Tooltips
{
    public class ItemTooltipHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
		[SerializeField] TooltipsController _tooltipsController;
		[SerializeField] bool _hideOnClick = true;
        IHaveItemTooltip _tooltipSource;

        void Awake()
        {
            _tooltipSource = GetComponent<IHaveItemTooltip>();
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
			if(_hideOnClick)
			{
				_tooltipsController.HideTooltip();
			}
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if(_tooltipSource is null || _tooltipSource.shouldShowTooltip == false) return;
			var tooltipData = _tooltipSource.GetTooltipData();
			_tooltipsController.ShowItemTooltip(tooltipData);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            _tooltipsController.HideTooltip();
        }
    }
}