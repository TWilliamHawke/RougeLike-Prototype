using System.Collections;
using System.Collections.Generic;
using Abilities;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]
    public class WeaponTemplate : ItemTemplate
    {
		[SerializeField] LocalString _weaponName;
		[SerializeField] int _baseValue = 1;
		[SerializeField] int _baseRarity = 0;
		[SerializeField] int _rarityStep = 10;

		[SerializeField] WeaponAbilityList _abilities;

		[SerializeField] List<WeaponQualityData> _qualities;

        public EquipmentTypes equipmentType => EquipmentTypes.weapon;

        public override IItem CreateItem(int rarity = 0)
        {
            var qualityData = GetQualityData(rarity);
            return new Weapon(this, qualityData);
        }

		public string ConstructName(WeaponQualityData qualityData)
		{
            if (string.IsNullOrEmpty(qualityData.levelName))
            {
                return _weaponName;
            }
			return qualityData.levelName + " " + _weaponName;
		}

		public int CalcValue(WeaponQualityData qualityData)
		{
			return Mathf.RoundToInt(qualityData.priceMult * _baseValue);
		}

        private WeaponQualityData GetQualityData(int rarity)
        {
            int rawIdx = Mathf.RoundToInt((rarity - _baseRarity) / (float)_rarityStep);
            int idx = Mathf.Clamp(rawIdx, 0, _qualities.Count - 1);
            return _qualities[idx];
        }

    }
}