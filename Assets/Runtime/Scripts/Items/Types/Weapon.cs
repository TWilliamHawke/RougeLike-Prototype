using System.Collections.Generic;
using Abilities;
using Effects;
using Items.Equipment;
using UnityEngine;

namespace Items
{
    public class Weapon : AbstractItem, IEquipment, IAbilitySource, IEffectSource
    {
        public IEquipmentSlotTemplate equipmentSlot => _weaponTemplate.equipmentSlot;

        protected override ItemTemplate _template => _weaponTemplate;

        public override string displayName => _weaponTemplate.ConstructName(_qualityData);
        public override int value => _weaponTemplate.CalcValue(_qualityData);

        WeaponTemplate _weaponTemplate;
        WeaponQualityData _qualityData;

        public Weapon(WeaponTemplate weaponTemplate, WeaponQualityData qualityData)
        {
            _weaponTemplate = weaponTemplate;
            _qualityData = qualityData;
        }

        public override string GetDescription()
        {
            throw new System.NotImplementedException();
        }

        public IAbilityContainer CreateAbilityContainer(IAbilitiesFactory factory)
        {
            var ability = _weaponTemplate.CreateAbility();
            ability.BindEffectSource(this);
            return factory.CreateEquipmentContainer(equipmentSlot, ability);
        }

        public IEnumerable<ISourceEffectData> GetEffects()
        {
            int damage = Random.Range(_qualityData.minDamage, _qualityData.maxDamage + 1);
            yield return new SourceEffectData(_weaponTemplate.damageType, damage, 0);
        }
    }
}