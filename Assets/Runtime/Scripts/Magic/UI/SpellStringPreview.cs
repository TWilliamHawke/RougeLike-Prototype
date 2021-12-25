using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Items;
using UnityEngine.UI;
using UI.DragAndDrop;

namespace Magic.UI
{
	public class SpellStringPreview : UIDataElement<ItemSlotData>, IDragDataSource<ItemSlotData>
	{
		[SerializeField] DragableUIElement<ItemSlotData> _dragableItemPrefab;
		[Header("UI Elements")]
		[SerializeField] Image _icon;
		[SerializeField] TextMeshProUGUI _title;
		[SerializeField] TextMeshProUGUI _count;

		ItemSlotData _itemSlotData;

        public ItemSlotData dragData => _itemSlotData;

        public DragableUIElement<ItemSlotData> dragableElementPrefab => _dragableItemPrefab;

        public override void UpdateData(ItemSlotData data)
        {
			_itemSlotData = data;
            _icon.sprite = data.item.icon;
			_title.text = data.item.displayName;
			_count.text = data.count.ToString();
        }

	}
}