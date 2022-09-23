using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Map.Objects.UI
{
	public class ActionButton : MonoBehaviour, IPointerClickHandler
	{
		[SerializeField] Image _actionIcon;
		[SerializeField] TextMeshProUGUI _actionTitle;

		IMapActionLogic _action;

        public void OnPointerClick(PointerEventData eventData)
        {
			if(_action is null) return;
            _action.DoAction();
        }

        public void SetAction(IMapActionLogic actionLogic)
		{
			_action = actionLogic;
			_actionIcon.sprite = actionLogic.template.icon;
			_actionTitle.text = actionLogic.template.displayName;
			this.gameObject.SetActive(true);
		}

		public void Hide()
		{
			this.gameObject.SetActive(false);
		}
	}
	    
}

