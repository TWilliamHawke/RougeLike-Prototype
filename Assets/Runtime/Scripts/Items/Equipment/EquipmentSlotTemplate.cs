using UnityEngine;

namespace Items.Equipment
{
	[CreateAssetMenu(fileName = "EquipmentSlotTemplate", menuName = "Items/EquipmentSlotTemplate")]
    public class EquipmentSlotTemplate : DisplayedObject
	{
		[SerializeField] EquipmentTypes _equipmentType = EquipmentTypes.none;

		public EquipmentTypes equipmentType => _equipmentType;
	}
}