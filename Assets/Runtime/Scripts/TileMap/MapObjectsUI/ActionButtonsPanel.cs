using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map.Objects.UI
{
	public class ActionButtonsPanel : MonoBehaviour
	{
		[SerializeField] ActionButton[] _actionButtons;

	    public void SetActions(IMapActionsController actionLogics)
		{
			for (int i = 0; i < _actionButtons.Length; i++)
			{
                if (i < actionLogics.count)
                {
                    _actionButtons[i].SetAction(actionLogics[i]);
                }
                else
                {
                    _actionButtons[i].Hide();
                }
            }
		}
	}
}

