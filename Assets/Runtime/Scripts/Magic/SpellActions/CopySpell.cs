using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Magic.Actions
{
    public class CopySpell : RadialActionFactory<KnownSpellData>
    {
        Spellbook _spellbook;

        protected override IRadialMenuAction CreateAction(KnownSpellData element)
        {
            throw new System.NotImplementedException();
        }

        protected override bool ElementIsValid(KnownSpellData element)
        {
            throw new System.NotImplementedException();
        }

        class CopySpellAction : IRadialMenuAction
        {
            public RadialButtonPosition preferedPosition => RadialButtonPosition.topRight;

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
