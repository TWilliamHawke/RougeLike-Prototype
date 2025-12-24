using System.Collections.Generic;
using Core;
using Entities.PlayerScripts;
using Items;
using Magic;
using UnityEngine;
using System.Linq;
using Items.Equipment;

namespace Abilities
{
    public class MainAbilityScreen : ScreenWithSections<AbilitySection>
    {
        [SerializeField] Inventory _inventory;
        [SerializeField] Spellbook _spellbook;
        [SerializeField] PlayerEquipment _equipment;
        [SerializeField] AbilitySection _abilitySectionPrefab;
        [SerializeField] LocalString _spellsSectionName;
        [SerializeField] LocalString _equipmentSectionName;
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

            CreateEquipmentSection();
            CreateSpellSection();

            foreach (var sectionTemplate in _inventorySections)
            {
                CreateItemAbilitySection(sectionTemplate);
            }
        }

        private void CreateEquipmentSection()
        {
            AbilitySectionData equipmentSection = new(_equipmentSectionName);
            foreach (var itemSlot in _equipment.GetAllItems())
            {
                if (itemSlot.item is not IAbilitySource abilitySource) continue;
                var container = abilitySource.CreateAbilityContainer(_abilitiesFactory);
                equipmentSection.AddMainSlotAbility(container);
            }

            CreateSection(equipmentSection);
        }

        private void CreateSpellSection()
        {
            AbilitySectionData spellsSection = new(_spellsSectionName);

            foreach (var knownSpell in _spellbook.knownSpells)
            {
                var ability = knownSpell.CreateAbilityContainer(_abilitiesFactory);
                spellsSection.AddMainSlotAbility(ability);
            }

            CreateSection(spellsSection);
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
                if (slot.item is not IAbilitySource abilitySource) continue;
                var container = abilitySource.CreateAbilityContainer(_abilitiesFactory);
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