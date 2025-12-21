namespace Items
{
    public class Weapon : AbstractItem, IEquipment
    {
        public EquipmentTypes equipmentType => EquipmentTypes.weapon;

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
    }
}