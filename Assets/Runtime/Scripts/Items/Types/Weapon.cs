namespace Items
{
    public class Weapon : AbstractItem
    {
        public override int value => _weaponTemplate.value;

        protected override IItemTemplate _template => _weaponTemplate;

        WeaponTemplate _weaponTemplate;

        public Weapon(WeaponTemplate weaponTemplate)
        {
            _weaponTemplate = weaponTemplate;
        }

        public override string GetDescription()
        {
            throw new System.NotImplementedException();
        }
    }
}