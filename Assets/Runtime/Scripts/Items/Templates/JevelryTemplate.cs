using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Jevelry", menuName = "Items/Jevelry")]
    public class JevelryTemplate : ItemTemplate, IEquipment
    {
		[SerializeField] EquipmentTypes _equipmentType;

        public EquipmentTypes equipmentType => _equipmentType;

        public override IItem CreateItem(int rarity = 0)
        {
            return new Jevelry(this);
        }

        public override string GetDescription()
        {
            return "Description";
        }
    }
}