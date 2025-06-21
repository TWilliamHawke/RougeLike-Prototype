using Abilities;
using Entities.Stats;
using Items;
using UnityEngine;

namespace Magic
{
    public class SpellContainer : IAbilityContainer
    {
        ISafeStatController _manaStorage;
        KnownSpellData _spellData;
        MagicConfig _magicConfig;
        StatsContainer _statsContainer;
        SpellDescriptionConstructor _descriptionConstructor;

        public Sprite icon => _spellData.icon;
        public int spellCost => _magicConfig.GetSpellCost(_spellData, _statsContainer);
        public bool canBeUsed => _manaStorage.currentValue >= spellCost;
        public string displayName => _spellData.displayName;
        public int numOfUses => -1;
        public KnownSpellData spellData => _spellData;

        public SpellContainer(KnownSpellData spellData, StatsContainer statsContainer, MagicConfig magicConfig)
        {
            _spellData = spellData;
            _magicConfig = magicConfig;
            _statsContainer = statsContainer;
            _manaStorage = magicConfig.FindManaStorage(statsContainer);
            _descriptionConstructor = new(spellData, statsContainer, _magicConfig);
        }

        public void UseAbility(AbilityController controller)
        {
            if (_manaStorage.TryReduceStat(spellCost))
            {
                _spellData.spellEffect.SelectAbilityController(controller);
            }
        }

        public bool HasSpell(KnownSpellData spell)
        {
            return _spellData == spell;
        }

        public string ConstructDescription()
        {
            return _descriptionConstructor.ConstructDescription();
        }

        public string GetRankUpDescription()
        {
            return _descriptionConstructor.GetRankUpDescription();
        }

        public string ConstructDescriptionWith(int slotIndex, SpellString spellString)
        {
            return _descriptionConstructor.ConstructDescriptionWith(slotIndex, spellString);
        }

        public string GetRankUpSpellCost()
        {
            return _descriptionConstructor.GetRankUpSpellCost();
        }

        public string GetSpellCostWith(int slotIndex, SpellString spellString)
        {
            return _descriptionConstructor.GetSpellCostWith(slotIndex, spellString);
        }
    }
}