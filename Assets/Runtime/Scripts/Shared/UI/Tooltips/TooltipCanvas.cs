using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Tooltips
{
    public class TooltipCanvas : MonoBehaviour
    {
        [SerializeField] SimpleTooltip _simpleTooltip;
		[SerializeField] ItemTooltip _itemTooltip;
        [SerializeField] TooltipsController _tooltipsController;

		Coroutine _tooltipTimeout;
		ITooltip _activeTooltip;

        public void Init()
        {
            _tooltipsController.OnSimpleTooltipShow += ShowSimpleTooltip;
            _tooltipsController.OnTooltipHide += HideTooltips;
			_tooltipsController.OnItemTooltipShow += ShowItemTooltip;
        }

        private void OnDestroy()
        {
            _tooltipsController.OnSimpleTooltipShow -= ShowSimpleTooltip;
            _tooltipsController.OnTooltipHide -= HideTooltips;
			_tooltipsController.OnItemTooltipShow -= ShowItemTooltip;
        }

		void Update()
		{
			if(_activeTooltip is null) return;
			_activeTooltip.UpdatePosition();
		}

        void ShowSimpleTooltip(string tooltipText)
        {
			_simpleTooltip.SetTooltipText(tooltipText);
			_activeTooltip?.Hide();
			_activeTooltip = _simpleTooltip;
			_tooltipTimeout = StartCoroutine(ShowAfterDelay());
        }

		void ShowItemTooltip(ItemTooltipData tooltipData)
		{
			_itemTooltip.SetTooltipData(tooltipData);
			_activeTooltip?.Hide();
			_activeTooltip = _itemTooltip;
			_tooltipTimeout = StartCoroutine(ShowAfterDelay());
		}

		IEnumerator ShowAfterDelay()
		{
			yield return new WaitForSeconds(_tooltipsController.delayTime);
			_activeTooltip?.Show();
		}

		void HideTooltips()
		{
			if(_tooltipTimeout != null)
			{
				StopCoroutine(_tooltipTimeout);
			}
			_activeTooltip?.Hide();
			_activeTooltip = null;
		}
    }
}