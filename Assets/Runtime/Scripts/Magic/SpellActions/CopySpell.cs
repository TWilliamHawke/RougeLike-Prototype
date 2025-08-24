using Core;

namespace Magic.Actions
{
    public class CopySpell : ContextActionFactory<KnownSpellData>
    {
        Spellbook _spellbook;

        public CopySpell(Spellbook spellbook)
        {
            _spellbook = spellbook;
        }

        protected override ContextActionContainer CreateAction(KnownSpellData element)
        {
            return new CopySpellAction(_spellbook, element);
        }

        protected override bool ElementIsValid(KnownSpellData element)
        {
            return true;
        }

        class CopySpellAction : ContextActionContainer
        {
            Spellbook _spellbook;
            KnownSpellData _spellData;

            public CopySpellAction(Spellbook spellbook, KnownSpellData spellData)
            {
                _spellbook = spellbook;
                _spellData = spellData;
            }

            public override void DoAction()
            {
                _spellbook.AddSpellCopy(_spellData);
            }
        }
    }
}
