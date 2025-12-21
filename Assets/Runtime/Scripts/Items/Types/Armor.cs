namespace Items
{
    public class Armor : AbstractItem
    {
        public override int value => 100;

        protected override ItemTemplate _template => _armorTemplate;

        public override string displayName => throw new System.NotImplementedException();

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