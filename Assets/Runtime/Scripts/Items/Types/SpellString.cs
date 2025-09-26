using System.Collections.Generic;
using Effects;

namespace Items
{
    public class SpellString : AbstractItem
    {
        public override int value => _spellStringTemplate.value;
        public IEnumerable<SourceEffectData> effects => _spellStringTemplate.effects;

        protected override IItemTemplate _template => _spellStringTemplate;

        SpellStringTemplate _spellStringTemplate;

        public SpellString(SpellStringTemplate spellStringTemplate)
        {
            _spellStringTemplate = spellStringTemplate;
        }

        public override string GetDescription()
        {
            return _spellStringTemplate.GetDescription();
        }
    }
}