using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Core.UI
{
	public class StatBar : MonoBehaviour
	{

		[SerializeField] Image _fillImage;
		[SerializeField] TextMeshProUGUI _statText;

		public void ChangeStat(int current, int max)
		{
			_fillImage.fillAmount = (float)current / max;
			_statText.text = $"{current}/{max}";
		}


	}
}