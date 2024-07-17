using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Entities.Stats;

namespace Core.UI
{
	public class StatBar : MonoBehaviour, IObserver<IResourceStorageData>
	{

		[SerializeField] Image _fillImage;
		[SerializeField] TextMeshProUGUI _statText;
        [SerializeField] StoredResource _observedStat;

        public StoredResource observedStat => _observedStat;

        public void AddToObserve(IResourceStorageData target)
        {
            ChangeStat(target.value, target.maxValue);
            target.OnValueChange += ChangeStat;
        }

        public void ChangeStat(int current, int max)
		{
			_fillImage.fillAmount = (float)current / max;
			_statText.text = $"{current}/{max}";
		}

        public void RemoveFromObserve(IResourceStorageData target)
        {
            target.OnValueChange -= ChangeStat;
        }
    }
}