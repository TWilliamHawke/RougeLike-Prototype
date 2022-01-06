using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Effects;
using UnityEngine.UI;
using TMPro;

namespace Core.UI
{
	public class ActiveEffect : UIDataElement<TemporaryEffectData>
	{
		[SerializeField] Color _positiveEffectColor = Color.green;
		[SerializeField] Color _negativeEffectColor = Color.red;
		[SerializeField] Image _effectIcon;
		[SerializeField] Image _effectFrame;
		[SerializeField] TextMeshProUGUI _turnsText;

        public override void UpdateData(TemporaryEffectData data)
        {
            _effectIcon.sprite = data.effect.icon;
			_turnsText.text = data.remainingDuration.ToString();
        }

        
	}
}