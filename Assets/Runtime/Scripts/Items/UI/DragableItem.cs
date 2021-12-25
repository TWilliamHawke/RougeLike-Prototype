using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI.DragAndDrop;
using UnityEngine.UI;

namespace Items.UI
{
	public class DragableItem : DragableUIElement<ItemSlotData>
	{
		[SerializeField] Image _itemIcon;

		ItemSlotData _itemSlotData;

        protected override void ApplyData(ItemSlotData data)
        {
            _itemIcon.sprite = data.item.icon;
			_itemSlotData = data;
        }

	}
}