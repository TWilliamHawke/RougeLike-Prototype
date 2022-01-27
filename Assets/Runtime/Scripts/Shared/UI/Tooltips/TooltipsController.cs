using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Tooltips
{
	public class TooltipsController : ScriptableObject
	{
		[Range(0, 1)]
		[SerializeField] float _delayTime = 0.1f;
	    public event UnityAction<ItemTooltipData> OnItemTooltipShow;
		public event UnityAction<string> OnSimpleTooltipShow;

		public float delayTime => _delayTime;

		public event UnityAction OnTooltipHide;

		public void ShowItemTooltip(ItemTooltipData tooltipData)
		{
			OnItemTooltipShow?.Invoke(tooltipData);
		}

		public void ShowSimpleTooltip(string tooltipText)
		{
			OnSimpleTooltipShow?.Invoke(tooltipText);
		}

		public void HideTooltip()
		{
			OnTooltipHide?.Invoke();
		}
	}
}