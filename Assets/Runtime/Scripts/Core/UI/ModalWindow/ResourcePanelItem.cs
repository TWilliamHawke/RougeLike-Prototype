using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Items;

namespace Core.UI
{
	public class ResourcePanelItem : UIDataElement<ItemSlotData>
	{
		[SerializeField] Image _icon;
		[SerializeField] TextMeshProUGUI _description;

        public override void UpdateData(ItemSlotData data)
        {
            _icon.sprite = data.item.icon;
			_description.text = data.item.displayName + ": x" + data.count;
        }
    }
}


