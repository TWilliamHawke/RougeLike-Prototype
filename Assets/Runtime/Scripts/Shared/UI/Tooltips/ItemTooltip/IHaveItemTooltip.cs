using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Tooltips
{
	public interface IHaveItemTooltip
	{
		bool shouldShowTooltip { get; }
	    ItemTooltipData GetTooltipData();
	}
}