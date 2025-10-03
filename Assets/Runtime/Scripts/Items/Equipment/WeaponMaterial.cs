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
	}
}