using System.Collections.Generic;
using Effects;

namespace Items
{
    public class SpellString : StaticItem
    {
        public override int value => _spellStringTemplate.value;
        public IEnumerable<SourceEffectData> effects => _spellStringTemplate.effects;

         protected override StaticItemTemplate _staticTemplate => _spellStringTemplate;

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