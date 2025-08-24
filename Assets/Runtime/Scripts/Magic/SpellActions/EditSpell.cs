using Core;
using Magic.UI;

namespace Magic.Actions
{
    public class EditSpell : ContextActionFactory<KnownSpellData>
    {
        SpellPage _spellPage;

        public EditSpell(SpellPage spellPage)
        {
            _spellPage = spellPage;
        }

        protected override ContextActionContainer CreateAction(KnownSpellData element)
        {
            return new EditSpellAction(element, _spellPage);
        }

        protected override bool ElementIsValid(KnownSpellData element)
        {
            return true;
        }

        class EditSpellAction : ContextActionContainer
        {
            KnownSpellData _spellData;
            SpellPage _spellPage;

            public EditSpellAction(KnownSpellData spellData, SpellPage spellPage)
            {
                _spellData = spellData;
                _spellPage = spellPage;
            }

            public override void DoAction()
            {
                _spellPage.Open(_spellData);
            }
        }
    }
}
