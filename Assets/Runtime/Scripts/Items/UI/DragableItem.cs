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

        public override void ApplyData(ItemSlotData data)
        {
            _itemIcon.sprite = data.item.icon;
        }

	}
}