using Abilities;
using Effects;
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
        StaticStatStorage _spellPowerStorage;
        StatsContainer _statsContainer;

        public Sprite icon => _spellData.icon;
        public bool canBeUsed => _manaStorage.currentValue >= _spellData.manaCost;
        public string displayName => _spellData.displayName;
        public int numOfUses => -1;
        public KnownSpellData spellData => _spellData;

        public SpellContainer(KnownSpellData spellData, StatsContainer statsContainer, MagicConfig magicConfig)
        {
            _spellData = spellData;
            _magicConfig = magicConfig;
            _statsContainer = statsContainer;
            _manaStorage = magicConfig.FindManaStorage(statsContainer);
            _spellPowerStorage = magicConfig.FindSpellPowerStorage(statsContainer);
        }

        public void UseAbility(AbilityController controller)
        {
            if (_manaStorage.TryReduceStat(_spellData.manaCost))
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
            var abilityMods = GetSpellModifiers(_spellData.activeEffects);
            return _spellData.spellEffect.GetDescription(abilityMods);
        }

        public string ConstructDescriptionWith(int slotIndex, SpellString spellString)
        {
            var oldAbilityMods = GetSpellModifiers(_spellData.activeEffects);
            var newSpellData = _spellData.CreateDeepCopy();
            newSpellData.SetActiveString(slotIndex, spellString);
            var newAbilityMods = GetSpellModifiers(newSpellData.activeEffects);
            return _spellData.spellEffect.GetDescription(oldAbilityMods) + "->\n" + newSpellData.spellEffect.GetDescription(newAbilityMods);
        }

        public int CalculateManaCost()
        {
            int baseManaCost = _spellData.baseManaCost;
            var activeEffects = _spellData.activeEffects;
            return _magicConfig.GetSpellCost(baseManaCost, _statsContainer, activeEffects);
        }


        AbilityModifiers GetSpellModifiers(IEffectsIterator spellData)
        {
            int rawSpellPower = _spellPowerStorage.GetAdjustedValue(spellData);
            Debug.Log(rawSpellPower);

            return new AbilityModifiers
            {
                magnitudeMult = rawSpellPower * 0.01f,
            };
        }

        
    }
}