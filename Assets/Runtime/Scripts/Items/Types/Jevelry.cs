using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Jevelry", menuName = "Items/Jevelry")]
    public class Jevelry : Item, IEquipment
    {
		[SerializeField] EquipmentTypes _equipmentType;

        public EquipmentTypes equipmentType => _equipmentType;

        public override string GetDescription()
        {
            return "Description";
        }

        public override string GetItemType()
        {
            return "Jevelry";
        }
    }
}