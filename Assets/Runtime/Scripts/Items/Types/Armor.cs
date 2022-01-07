using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
	[CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor")]
    public class Armor : Item, IEquipment
    {
		[SerializeField] EquipmentTypes _equipmentType;
		[SerializeField] int _physicalResist;

        EquipmentTypes IEquipment.equipmentType => _equipmentType;
    }
}