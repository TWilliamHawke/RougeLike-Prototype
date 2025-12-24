using System.Collections;
using System.Collections.Generic;
using Abilities;
using Items.Equipment;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]
    public class WeaponTemplate : EquipmentTemplate<WeaponQualityData>
    {
        [SerializeField] EquipmentSlotTemplate _equipmentSlot;
		[SerializeField] WeaponAbilityList _abilities;

        public EquipmentSlotTemplate equipmentSlot => _equipmentSlot;

        public override IItem CreateItem(int rarity = 0)
        {
            var qualityData = GetQualityData(rarity);
            return new Weapon(this, qualityData);
        }
    }
}