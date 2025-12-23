using System.Collections.Generic;
using UnityEngine;

namespace Items.Equipment
{
    public abstract class EquipmentTemplate<T> : ItemTemplate where T : IEquipmentQualityData
	{
		[SerializeField] LocalString _displayName;
		[SerializeField] int _baseValue = 1;
		[SerializeField] int _baseRarity = 0;
		[SerializeField] int _rarityStep = 10;

		[SerializeField] List<T> _qualities;

		public string ConstructName(T qualityData)
		{
            if (string.IsNullOrEmpty(qualityData.displayName))
            {
                return _displayName;
            }
			return qualityData.displayName + " " + _displayName;
		}

		public int CalcValue(T qualityData)
		{
			return Mathf.RoundToInt(qualityData.priceMult * _baseValue);
		}

        protected T GetQualityData(int rarity)
        {
            int rawIdx = Mathf.RoundToInt((rarity - _baseRarity) / (float)_rarityStep);
            int idx = Mathf.Clamp(rawIdx, 0, _qualities.Count - 1);
            return _qualities[idx];
        }

    }
}