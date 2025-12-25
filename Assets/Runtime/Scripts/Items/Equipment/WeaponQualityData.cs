using UnityEngine;

namespace Items.Equipment
{
    [System.Serializable]
	public class WeaponQualityData : IEquipmentQualityData
	{
		[SerializeField] EquipmentQualityLevel _levelName;
		[SerializeField] IntValue _damage;
		[SerializeField] int _criticalDamage;

		public float priceMult => _levelName.priceMult;
        public string displayName => _levelName.displayName;
		public int maxDamage => _damage.maxValue;
		public int minDamage => _damage.minValue;
		public int criticalDamage => _criticalDamage;
    }
}