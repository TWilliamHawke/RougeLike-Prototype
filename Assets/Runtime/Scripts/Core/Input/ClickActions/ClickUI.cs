using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System.Linq;

namespace Core.Input
{
    public class ClickUI : IClickAction
    {

        const string IGNORE_RAYCAST_TAG = "IgnoreUIRaycast";

        void IClickAction.ProcessClick()
        {
            //do nothing
        }

        bool IClickAction.Condition()
        {
            var hits = Raycasts.UI();

            if (hits.Any(hit => !hit.gameObject.CompareTag(IGNORE_RAYCAST_TAG)))
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