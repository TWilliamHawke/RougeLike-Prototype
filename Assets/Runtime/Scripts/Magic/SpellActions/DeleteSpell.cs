using System.Collections;
using System.Collections.Generic;
using Core;
using Core.UI;
using Items;
using UnityEngine;

namespace Magic.Actions
{
    public class DeleteSpell : RadialActionFactory<KnownSpellData>
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

        protected override IRadialMenuAction CreateAction(KnownSpellData element)
        {
            ModalWindowData modalWindowData = new()
            {
                title = TITLE,
                mainText = MAIN_TEXT,
                mainImage = element.icon,
                action = new DeleteSpellAction(_spellbook, element, _inventory)
            };

            return new OpenModalWindowRadial(
                modalWindow: _modalWindow,
                modalWindowData: modalWindowData,
                preferedPosition: RadialButtonPosition.bottom,
                actionTitle: ACTION_TITLE
            );
        }

        protected override bool ElementIsValid(KnownSpellData element)
        {
            return _spellbook.GetCountSpellsOfType(element) > 1;
        }

        class DeleteSpellAction : IContextAction
        {
            Spellbook _spellbook;
            KnownSpellData _spellData;
            Inventory _inventory;

            public string actionTitle => "Confirm";

            public DeleteSpellAction(Spellbook spellbook, KnownSpellData spellData, Inventory inventory)
            {
                _spellbook = spellbook;
                _spellData = spellData;
                _inventory = inventory;
            }

            public void DoAction()
            {
                foreach (var stringSlot in _spellData.activeStrings)
                {
                    if (!stringSlot.IsEmpty())
                    {
                        _inventory.AddItem(stringSlot.spellString);
                    }
                }

                _spellbook.DeleteSpell(_spellData);
            }
        }
    }
}
