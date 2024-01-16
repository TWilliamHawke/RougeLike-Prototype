using System.Collections;
using System.Collections.Generic;
using Core;
using Items;
using UnityEngine;

namespace Magic.Actions
{
    public class CopySpell : RadialActionFactory<KnownSpellData>
    {
        Spellbook _spellbook;

        public CopySpell(Spellbook spellbook)
        {
            _spellbook = spellbook;
        }

        protected override IRadialMenuAction CreateAction(KnownSpellData element)
        {
            return new CopySpellAction(_spellbook, element);
        }

        protected override bool ElementIsValid(KnownSpellData element)
        {
            return true;
        }

        class CopySpellAction : IRadialMenuAction
        {
            public RadialButtonPosition preferedPosition => RadialButtonPosition.bottomRight;

            public string actionTitle => "Copy Spell";

            Spellbook _spellbook;
            KnownSpellData _spellData;

            public CopySpellAction(Spellbook spellbook, KnownSpellData spellData)
            {
                _spellbook = spellbook;
                _spellData = spellData;
            }

            public void DoAction()
            {
                _spellbook.AddSpellCopy(_spellData);
            }
        }
    }
}
