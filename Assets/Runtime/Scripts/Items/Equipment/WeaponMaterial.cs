using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	[CreateAssetMenu(fileName = "WeaponMaterial", menuName = "Items/WeaponMaterial")]
    public class WeaponMaterial : ScriptableObject
	{
		[SerializeField] int _baseValue = 1;
		[SerializeField] int _baseRarity = 1;
		[SerializeField] int _rarityStep = 1;

		[SerializeField] List<WeaponQualityData> _qualities = new List<WeaponQualityData>();

		public int baseValue => _baseValue;
		public int baseRarity => _baseRarity;
		public int maxRarity => _qualities.Count * _rarityStep + _baseRarity;
	}
}