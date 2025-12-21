using UnityEngine;

namespace Items
{
    [System.Serializable]
	public class WeaponQualityData : IEquipmentQualityData
	{
		[SerializeField] EquipmentQualityLevel _levelName;
		[SerializeField] IntValue _damage;
		[SerializeField] int _criticalDamage;

		public float priceMult => _levelName.priceMult;
        public string displayName => _levelName.displayName;
    }
}