using UnityEngine;

namespace Items.Equipment
{
	[CreateAssetMenu(fileName = "Equipment Quality", menuName = "Items/Equipment Quality")]
	public class EquipmentQualityLevel : ScriptableObject
	{
		[SerializeField] LocalString _displayName;
		[Range(0f, 3f)]
		[SerializeField] float _priceMult = 1f;

		public LocalString displayName => _displayName;
		public float priceMult => _priceMult;
	}
}