using System.Collections.Generic;
using Abilities;
using Effects;
using Items.Equipment;

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
            return factory.CreateEquipmentContainer(equipmentSlot, ability);
        }

        public IEnumerable<ISourceEffectData> GetEffects()
        {
            throw new System.NotImplementedException();
        }
    }
}