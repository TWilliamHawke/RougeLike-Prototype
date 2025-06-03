using System.Collections;
using System.Collections.Generic;
using Core;
using Magic.UI;
using UnityEngine;

namespace Magic.Actions
{
    public class EditSpell : RadialActionFactory<SpellContainer>
    {
        SpellPage _spellPage;

        public EditSpell(SpellPage spellPage)
        {
            _spellPage = spellPage;
        }

        protected override IRadialMenuAction CreateAction(SpellContainer element)
        {
            return new EditSpellAction(element.spellData, _spellPage);
        }

        protected override bool ElementIsValid(SpellContainer element)
        {
            return true;
        }

        class EditSpellAction : IRadialMenuAction
        {
            KnownSpellData _spellData;
            SpellPage _spellPage;

            public EditSpellAction(KnownSpellData spellData, SpellPage spellPage)
            {
                _spellData = spellData;
                _spellPage = spellPage;
            }

            public RadialButtonPosition preferedPosition => RadialButtonPosition.top;

            public string actionTitle => "Edit Spell";

            public void DoAction()
            {
                _spellPage.Open(_spellData);
            }
        }
    }
}
