using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

namespace Items
{
	public class ItemSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
	{
		[SerializeField] Sprite _emptyFrame;
		[Header("UI Elements")]
		[SerializeField] Image _icon;
		[SerializeField] Image _frame;
		[SerializeField] Image _background;
		[SerializeField] Image _outline;
		[SerializeField] TextMeshProUGUI _count;

		ItemSlotData _slotData;



	    public void SetSlotData(ItemSlotData slotData)
		{
			_icon.gameObject.SetActive(true);
			_frame.gameObject.SetActive(false);
			_background.gameObject.SetActive(true);

			_slotData = slotData;

			_icon.sprite = slotData.item.icon;

			if(slotData.item.maxStackCount > 1)
			{
				_count.gameObject.SetActive(true);
				_count.text = slotData.count.ToString();
			}
		}

		public void Clear()
		{
			_slotData = null;
			_icon.gameObject.SetActive(false);
			_background.gameObject.SetActive(false);
			_count.gameObject.SetActive(false);
			_frame.gameObject.SetActive(true);
		}

        public void OnPointerEnter(PointerEventData eventData)
        {
			if(_slotData == null) return;
            _outline.gameObject.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            _outline.gameObject.SetActive(false);
        }
    }
}