using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Items.UI
{
    public class ItemActionButton : MonoBehaviour, IPointerClickHandler
    {
		public static event UnityAction OnClick;

		IItemAction _boundedAction;
		IItemSlot _selectedSlot;
		
		[SerializeField] Text _buttonText;

		public void BindAction(IItemAction action)
		{
			_boundedAction = action;
			_buttonText.text = action.actionTitle;
		}

		public void CheckItemSlot(IItemSlot itemSlot)
		{
			gameObject.SetActive(_boundedAction.SlotIsValid(itemSlot));
			_selectedSlot = itemSlot;
		}

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            if(_selectedSlot == null) return;
			_boundedAction.DoAction(_selectedSlot.itemSlotData);
			OnClick?.Invoke();
        }
    }
}