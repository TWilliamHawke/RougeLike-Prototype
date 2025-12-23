using UnityEngine;

namespace Items.Equipment
{
	[System.Serializable]
    public class ArmorQualityData : IEquipmentQualityData
	{
		[SerializeField] EquipmentQualityLevel _levelName;
		[SerializeField] int _armor;
		[SerializeField] int _heaviness;

		public float priceMult => _levelName.priceMult;
		public string displayName => _levelName.displayName;
	}
}