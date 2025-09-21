using System.Collections.Generic;
using Core;
using Entities.PlayerScripts;
using Items;
using Magic;
using UnityEngine;
using System.Linq;

namespace Abilities
{
    public class MainAbilityScreen : ScreenWithSections<AbilitySection>
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] Spellbook _spellbook;
        [SerializeField] AbilitySection _abilitySectionPrefab;
        [SerializeField] LocalString _spellsSectionName;
        [SerializeField] List<ItemSectionTemplate> _inventorySections;

        [InjectField] Player _player;
        PlayerAbilitiesFactory _abilitiesFactory;

        [SerializeField] ContextActionTemplate _useAction;

        [Header("UI Elements")]
        [SerializeField] AbilitySectionsLayout _sectionsLayout;

        protected override IObserversController<AbilitySection> _layout => _sectionsLayout;

        public override void OpenScreen()
        {
            CreateSections();
            base.OpenScreen();
        }

        public void CreateAbilityFactory()
        {
            _abilitiesFactory = _player.GetEntityComponent<PlayerAbilitiesFactory>();
            CreateSections();
        }

        protected override void CreateSections()
        {
            if (_abilitiesFactory == null) return;
            _sectionsLayout.ClearLayout();

            AbilitySectionData spells = new(_spellsSectionName);

            foreach (var knownSpell in _spellbook.knownSpells)
            {
                var ability = knownSpell.CreateAbilityContainer(_abilitiesFactory);
                spells.AddMainSlotAbility(ability);
            }

            CreateSection(spells);

            foreach (var sectionTemplate in _inventorySections)
            {
                CreateItemAbilitySection(sectionTemplate);
            }
        }

        private void CreateItemAbilitySection(ItemSectionTemplate sectionTemplate)
        {
            var itemSection = _inventory.GetSection(sectionTemplate);
            if (itemSection == null) return;
            AbilitySectionData abilitiesList = new(sectionTemplate.name);

            foreach (ItemSlotData slot in itemSection)
            {
                var slotActions = slot.GetActions(sectionTemplate);
                if (slotActions.All(action => action != _useAction)) continue;
                var item = slot.item as IAbilitySource;
                if (item == null) continue;
                var container = item.CreateAbilityContainer(_abilitiesFactory);
                abilitiesList.AddMainSlotAbility(container);
            }

            CreateSection(abilitiesList);
        }

        private void CreateSection(AbilitySectionData abilitiesList)
        {
            if (abilitiesList.isEmpty) return;
            var section = _sectionsLayout.CreateLayoutElement(_abilitySectionPrefab);
            section.BindData(abilitiesList);
        }
    }
}