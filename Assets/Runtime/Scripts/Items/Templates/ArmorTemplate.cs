using System.Collections;
using System.Collections.Generic;
using Items.Equipment;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Armor", menuName = "Items/Armor")]
    public class ArmorTemplate : EquipmentTemplate<ArmorQualityData>
    {
        [SerializeField] EquipmentSlotTemplate _equipmentSlot;

        public EquipmentSlotTemplate equipmentSlot => _equipmentSlot;

        public override IItem CreateItem(int rarity = 0)
        {
            var qualityData = GetQualityData(rarity);
            return new Armor(this, qualityData);
        }
    }
}