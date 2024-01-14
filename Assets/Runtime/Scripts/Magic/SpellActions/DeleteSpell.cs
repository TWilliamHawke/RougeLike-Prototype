using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Magic.Actions
{
    public class DeleteSpell : RadialActionFactory<KnownSpellData>
    {
        Spellbook _spellbook;

        public DeleteSpell(Spellbook spellbook)
        {
            _spellbook = spellbook;
        }

        protected override IRadialMenuAction CreateAction(KnownSpellData element)
        {
            return new DeleteSpellAction(_spellbook, element);
        }

        protected override bool ElementIsValid(KnownSpellData element)
        {
            return _spellbook.GetCountSpellsOfType(element) > 1;
        }

        class DeleteSpellAction : IRadialMenuAction
        {
            Spellbook _spellbook;
            KnownSpellData _spellData;

            public RadialButtonPosition preferedPosition => RadialButtonPosition.bottom;
            public string actionTitle => "Delete Spell";

            public DeleteSpellAction(Spellbook spellbook, KnownSpellData spellData)
            {
                _spellbook = spellbook;
                _spellData = spellData;
            }

            public void DoAction()
            {
                _spellbook.DeleteSpell(_spellData);
            }
        }
    }
}
