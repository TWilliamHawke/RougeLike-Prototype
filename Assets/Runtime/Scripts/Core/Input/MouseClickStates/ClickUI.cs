using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Core.Input
{
	public class ClickUI : IMouseClickState
	{

		const string IGNORE_RAYCAST_TAG = "IgnoreUIRaycast";

        void IMouseClickState.ProcessClick()
        {
			//do nothing
        }

        bool IMouseClickState.Condition()
        {
            var hits = new List<RaycastResult>();
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Mouse.current.position.ReadValue();
            EventSystem.current.RaycastAll(eventData, hits);

			foreach (var hit in hits)
			{
				if(hit.gameObject.tag == IGNORE_RAYCAST_TAG)
				{
					continue;
				}
				else
				{
					return true;
				}
			}

			return false;

        }

	}
}