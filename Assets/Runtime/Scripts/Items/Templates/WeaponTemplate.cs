using System.Collections;
using System.Collections.Generic;
using Abilities;
using Effects;
using Items.Equipment;
using UnityEngine;

namespace Items
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Items/Weapon")]
    public class WeaponTemplate : EquipmentTemplate<WeaponQualityData>
    {
		[SerializeField] WeaponAbilityList _abilities;
		[SerializeField] DamageStoredResource _damageType;

        public IEquipmentSlotTemplate equipmentSlot => new WeaponEquipmentSlot(this);
        public DamageStoredResource damageType => _damageType;

        public override IItem CreateItem(int rarity = 0)
        {
            var qualityData = GetQualityData(rarity);
            return new Weapon(this, qualityData);
        }

        public AbstractAbility CreateAbility()
        {
            return _abilities.baseAbility.CreateAbility();
        }
    }
}