using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.UI;
using Magic.UI;
using Core;

namespace Magic.Actions
{
    using FactoryList = List<IActionFactory<KnownSpellData>>;

    [RequireComponent(typeof(ComponentInjector))]
    public class SpellActionsController : ActionController<KnownSpellData>, IObserver<KnownSpellSlot>
    {
        [SerializeField] Spellbook _spellbook;
        [SerializeField] SpellPage _spellPage;
        [SerializeField] SpellList _spellList;

        void Start()
        {
            _spellList.AddObserver(this);
            CreateFactory();
        }

        protected override void FillFactory(FactoryList factory)
        {
            factory.Add(new ShowInfo<KnownSpellData>());
            factory.Add(new BindToQuickbar<KnownSpellData>());
            factory.Add(new DeleteSpell(_spellbook));
            factory.Add(new EditSpell(_spellPage));
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
