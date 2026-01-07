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
    [RequireComponent(typeof(ComponentInjector))]
    public class SpellActionsController : ActionController<KnownSpellData>, IObserver<KnownSpellSlot>,
        IContextActionSource
    {
        [SerializeField] Spellbook _spellbook;
        [SerializeField] SpellPage _spellEditor;
        [SerializeField] SpellList _spellList;
        [SerializeField] Inventory _inventory;
        [SerializeField] ModalWindowController _modalWindow;
        [SerializeField] QuickBarSetupController _quickBarSetupController;
        [Header("Actions")]
        [SerializeField] ContextActionTemplate _useAbility;
        [SerializeField] ContextActionTemplate _showInfo;
        [SerializeField] ContextActionTemplate _bindToQuickbar;
        [SerializeField] ContextActionTemplate _deleteSpell;
        [SerializeField] ContextActionTemplate _editSpell;
        [SerializeField] ContextActionTemplate _copySpell;
        [SerializeField] ContextActionList _actionList;

        [InjectField] Player _player;

        void Start()
        {
            _spellList.AddObserver(this);
            CreateFactory();
        }

        protected override void FillFactory()
        {
            var abilitiesFactory = _player.GetEntityComponent<PlayerAbilitiesFactory>();
            var abilitiesController = _player.GetEntityComponent<AbilityController>();
            AddFactory(_showInfo, new ShowInfo<KnownSpellData>());
            AddFactory(_bindToQuickbar, new BindToQuickSlot(
               abilitiesFactory, _quickBarSetupController));
            AddFactory(_deleteSpell, new DeleteSpell(_spellbook, _inventory, _modalWindow));
            AddFactory(_editSpell, new EditSpell(_spellEditor));
            AddFactory(_copySpell, new CopySpell(_spellbook));
            AddFactory(_useAbility, new UseSpell(abilitiesFactory, abilitiesController));   
        }

        public void AddToObserve(KnownSpellSlot target)
        {
            target.OnSpellSelect += FillContextMenu;
        }

        public void RemoveFromObserve(KnownSpellSlot target)
        {
            target.OnSpellSelect -= FillContextMenu;
        }

        public IEnumerable<ContextActionTemplate> GetActions()
        {
            return _actionList.GetElements();
        }

        private void FillContextMenu(KnownSpellData actionSource)
        {
            FillContextMenu(actionSource, _actionList.GetElements());
        }
    }
}
