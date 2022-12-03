using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Core.UI
{
	public class ResourcePanelItem : MonoBehaviour
	{
		[SerializeField] Image _icon;
		[SerializeField] TextMeshProUGUI _description;


		public void BindData(Sprite icon, string description)
		{
			_icon.sprite = icon;
			_description.text = description;
		}
	}
}


