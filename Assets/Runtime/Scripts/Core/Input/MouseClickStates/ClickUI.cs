using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Linq;

namespace Core.Input
{
	public class ClickUI : IMouseClickAction
	{

		const string IGNORE_RAYCAST_TAG = "IgnoreUIRaycast";

        void IMouseClickAction.ProcessClick()
        {
			//do nothing
        }

        bool IMouseClickAction.Condition()
        {
            var hits = Raycasts.UI();

			if(hits.Any(hit => hit.gameObject.tag != IGNORE_RAYCAST_TAG))
			{
				return true;
			}
			else
			{
				return false;
			}

        }

	}
}