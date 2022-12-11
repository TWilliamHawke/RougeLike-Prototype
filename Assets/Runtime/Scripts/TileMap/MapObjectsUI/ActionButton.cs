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
		[SerializeField] Sprite _activeBg;
		[SerializeField] Sprite _inactiveBg;
		[Header("UI Elements")]
		[SerializeField] Image _actionIcon;
		[SerializeField] TextMeshProUGUI _actionTitle;
		[SerializeField] Image _buttonBg;
		[SerializeField] Button _button;

		IMapAction _action;

        public void OnPointerClick(PointerEventData eventData)
        {
			if(_action is null || !_action.isEnable) return;
            _action.DoAction();
        }

        public void SetAction(IMapAction actionLogic)
		{
			_action = actionLogic;
			_actionIcon.sprite = actionLogic.icon;
			_actionTitle.text = actionLogic.actionTitle;
			this.gameObject.SetActive(true);
			_button.interactable = actionLogic.isEnable;

			if(actionLogic.isEnable)
			{
				_buttonBg.sprite = _activeBg;
			}
			else
			{
				_buttonBg.sprite = _inactiveBg;
			}
		}

		public void Hide()
		{
			this.gameObject.SetActive(false);
		}
	}
	    
}

