using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using Magic.UI;
using Core;
using Items;
using Entities.PlayerScripts;
using Abilities;

namespace Magic.Actions
{
    using FactoryList = List<IActionFactory<SpellContainer>>;

    [RequireComponent(typeof(ComponentInjector))]
    public class SpellActionsController : ActionController<SpellContainer>, IObserver<KnownSpellSlot>
    {
        [SerializeField] Spellbook _spellbook;
        [SerializeField] SpellPage _spellEditor;
        [SerializeField] SpellList _spellList;
        [SerializeField] Inventory _inventory;
        [SerializeField] ModalWindowController _modalWindow;
        [SerializeField] QuickBarSetupController _quickBarSetupController;

        [InjectField] Player _player;

        void Start()
        {
            _spellList.AddObserver(this);
            CreateFactory();
        }

        protected override void FillFactory(FactoryList factory)
        {
            factory.Add(new ShowInfo<SpellContainer>());
            factory.Add(new BindToQuickbar<SpellContainer>(
                _player.GetComponent<PlayerAbilitiesFactory>(), _quickBarSetupController));
            factory.Add(new DeleteSpell(_spellbook, _inventory, _modalWindow));
            factory.Add(new EditSpell(_spellEditor));
            factory.Add(new CopySpell(_spellbook));
        }

        public void AddToObserve(KnownSpellSlot target)
        {
            target.OnDragStart += FillContextMenu;
        }

        public void RemoveFromObserve(KnownSpellSlot target)
        {
            target.OnDragStart -= FillContextMenu;
        }

    }
}
