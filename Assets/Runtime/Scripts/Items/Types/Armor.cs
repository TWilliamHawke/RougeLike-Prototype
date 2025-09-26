namespace Items
{
    public class Armor : AbstractItem
    {
        public override int value => _armorTemplate.value;

        protected override IItemTemplate _template => _armorTemplate;

        ArmorTemplate _armorTemplate;

        public Armor(ArmorTemplate armorTemplate)
        {
            _armorTemplate = armorTemplate;
        }

        public override string GetDescription()
        {
            throw new System.NotImplementedException();
        }
    }
}