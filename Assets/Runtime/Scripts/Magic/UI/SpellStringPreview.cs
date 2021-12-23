using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Items;
using UnityEngine.UI;

namespace Magic.UI
{
	public class SpellStringPreview : UIDataElement<ItemSlotData>
	{
		[Header("UI Elements")]
		[SerializeField] Image _icon;
		[SerializeField] TextMeshProUGUI _title;
		[SerializeField] TextMeshProUGUI _count;


        public override void UpdateData(ItemSlotData data)
        {
            _icon.sprite = data.item.icon;
			_title.text = data.item.displayName;
			_count.text = data.count.ToString();
        }

	}
}