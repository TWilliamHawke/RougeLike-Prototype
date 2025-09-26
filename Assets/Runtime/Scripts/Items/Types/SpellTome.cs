namespace Items
{
    public class SpellTome : AbstractItem
    {
        public override int value => _spellTomeTemplate.value;

        protected override IItemTemplate _template => _spellTomeTemplate;

        SpellTomeTemplate _spellTomeTemplate;

        public SpellTome(SpellTomeTemplate spellTomeTemplate)
        {
            _spellTomeTemplate = spellTomeTemplate;
        }

        public override string GetDescription()
        {
            return _spellTomeTemplate.GetDescription();
        }
    }
}