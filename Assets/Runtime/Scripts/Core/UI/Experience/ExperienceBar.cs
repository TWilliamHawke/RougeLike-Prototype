using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
	public class ExperienceBar : MonoBehaviour
	{
	    [SerializeField] Image _barFillImage;

		public void SetExpPct(float pct)
		{
			_barFillImage.fillAmount = pct;
		}
	}
}

