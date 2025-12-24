using UnityEngine;

namespace Items.Equipment
{
    [CreateAssetMenu(fileName = "EquipmentSlotTemplate", menuName = "Items/EquipmentSlotTemplate")]
    public class EquipmentSlotTemplate : DisplayedObject, IEquipmentSlotTemplate
    {
        [SerializeField] EquipmentTypes _equipmentType = EquipmentTypes.none;

        public int index => (int)_equipmentType;
    }
}