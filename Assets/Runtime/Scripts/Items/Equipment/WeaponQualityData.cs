using UnityEngine;

namespace Items
{
    [System.Serializable]
	public class WeaponQualityData
	{
		[SerializeField] EquipmentQualityLevel _levelName;
		[SerializeField] IntValue _damage;
		[SerializeField] int _criticalDamage;
	}
}