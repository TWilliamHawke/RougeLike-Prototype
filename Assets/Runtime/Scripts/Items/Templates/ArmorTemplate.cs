using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor")]
    public class ArmorTemplate : ItemTemplate, IEquipment
    {
        [SerializeField] EquipmentTypes _equipmentType;
        [SerializeField] int _physicalResist;

        EquipmentTypes IEquipment.equipmentType => _equipmentType;

        public override string GetDescription()
        {
            return $"Physical resistance: {_physicalResist}";
        }

        public override IItem CreateItem(int rarity = 0)
        {
            return new Armor(this);
        }
    }
}