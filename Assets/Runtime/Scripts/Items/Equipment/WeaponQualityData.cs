using UnityEngine;

namespace Items
{
    [System.Serializable]
	public class WeaponQualityData
	{
		[SerializeField] EquipmentQualityLevel _levelName;
		[SerializeField] IntValue _damage;
		[SerializeField] int _criticalDamage;

		public string levelName => _levelName.displayName;
		public float priceMult => _levelName.priceMult;
	}
}