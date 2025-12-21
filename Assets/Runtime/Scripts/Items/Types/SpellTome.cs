namespace Items
{
    public class SpellTome : StaticItem
    {
        public override int value => _spellTomeTemplate.value;

         protected override StaticItemTemplate _staticTemplate => _spellTomeTemplate;

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