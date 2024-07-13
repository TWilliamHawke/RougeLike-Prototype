using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Entities.Stats;

namespace Core.UI
{
	public class StatBar : MonoBehaviour, IObserver<IStatValues>
	{

		[SerializeField] Image _fillImage;
		[SerializeField] TextMeshProUGUI _statText;
        [SerializeField] StoredResource _observedStat;

        public StoredResource observedStat => _observedStat;

        IStatValues _stat;

        public void AddToObserve(IStatValues target)
        {
            _stat = target;
            UpdateBar();
            _stat.OnValueChange += UpdateBar;
        }

        private void UpdateBar(int _ = 0)
        {
            ChangeStat(_stat.currentValue, _stat.maxValue);
        }

        public void ChangeStat(int current, int max)
		{
			_fillImage.fillAmount = (float)current / max;
			_statText.text = $"{current}/{max}";
		}

        public void RemoveFromObserve(IStatValues target)
        {
            target.OnValueChange -= UpdateBar;
        }
    }
}