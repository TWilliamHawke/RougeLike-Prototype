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
        //[SerializeField] ContextActionTemplate _useAbility;
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
            var abilitiesFactory = _player.GetComponent<PlayerAbilitiesFactory>();
            AddFactory(_showInfo, new ShowInfo<KnownSpellData>());
            AddFactory(_bindToQuickbar, new BindToQuickbar<KnownSpellData>(
               abilitiesFactory, _quickBarSetupController));
            AddFactory(_deleteSpell, new DeleteSpell(_spellbook, _inventory, _modalWindow));
            AddFactory(_editSpell, new EditSpell(_spellEditor));
            AddFactory(_copySpell, new CopySpell(_spellbook));
            //factory.Add(new UseAbility(abilitiesFactory, abilityController,   
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
            return _actionList;
        }

        private void FillContextMenu(KnownSpellData actionSource)
        {
            FillContextMenu(actionSource, this);
        }
    }
}
