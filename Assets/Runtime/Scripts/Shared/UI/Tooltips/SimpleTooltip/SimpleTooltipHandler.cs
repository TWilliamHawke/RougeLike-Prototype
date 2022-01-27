using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Tooltips
{
    public class SimpleTooltipHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
    {
        [SerializeField] TooltipsController _tooltipsController;
        [SerializeField] bool _hideOnClick = true;

        IHaveSimpleTooltip _tooltipSource;

        private void OnEnable()
        {
            if (_tooltipSource != null) return;
            _tooltipSource = GetComponent<IHaveSimpleTooltip>();
        }

        void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
        {
            if (_hideOnClick)
            {
                _tooltipsController.HideTooltip();
            }
        }

        void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
        {
            if (_tooltipSource is null) return;
            string tooltipText = _tooltipSource.GetTooltipText();
            _tooltipsController.ShowSimpleTooltip(tooltipText);
        }

        void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
        {
            _tooltipsController.HideTooltip();
        }
    }
}