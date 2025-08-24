using Core;
using Core.UI;
using Items;

namespace Magic.Actions
{
    public class DeleteSpell : ContextActionFactory<KnownSpellData>
    {
        Spellbook _spellbook;
        Inventory _inventory;
        ModalWindowController _modalWindow;

        const string TITLE = "Delete this spell?";
        const string MAIN_TEXT = "You will lose some magic dust";
        const string ACTION_TITLE = "Delete Copy";

        public DeleteSpell(Spellbook spellbook, Inventory inventory, ModalWindowController modalWindow)
        {
            _spellbook = spellbook;
            _inventory = inventory;
            _modalWindow = modalWindow;
        }

        protected override ContextActionContainer CreateAction(KnownSpellData element)
        {
            ModalWindowData modalWindowData = new()
            {
                title = TITLE,
                mainText = MAIN_TEXT,
                mainImage = element.icon,
                action = new DeleteSpellAction(
                    _spellbook, element, _inventory),
            };

            return new OpenModalWindow(
                modalWindow: _modalWindow,
                modalWindowData: modalWindowData
            );
        }

        protected override bool ElementIsValid(KnownSpellData element)
        {
            return _spellbook.GetCountSpellsOfType(element) > 1;
        }

        class DeleteSpellAction : ContextActionContainer
        {
            Spellbook _spellbook;
            KnownSpellData _spellData;
            Inventory _inventory;

            public DeleteSpellAction(Spellbook spellbook, KnownSpellData spellData, Inventory inventory)
            {
                _spellbook = spellbook;
                _spellData = spellData;
                _inventory = inventory;
            }

            public override void DoAction()
            {
                _spellData.ClearAllSlots(_inventory);
                _spellbook.DeleteSpell(_spellData);
            }
        }
    }
}
