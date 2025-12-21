using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor")]
    public class ArmorTemplate : EquipmentTemplate<ArmorQualityData>
    {
        [SerializeField] EquipmentTypes _equipmentType;

        public EquipmentTypes equipmentType => _equipmentType;

        public override IItem CreateItem(int rarity = 0)
        {
            var qualityData = GetQualityData(rarity);
            return new Armor(this, qualityData);
        }
    }
}