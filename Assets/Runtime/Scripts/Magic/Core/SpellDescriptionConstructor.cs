using Abilities;
using Effects;
using Entities.Stats;
using Items;
using UnityEngine;

namespace Magic
{
    public class SpellDescriptionConstructor
    {
        KnownSpellData _spellData { get; init; }
        StatContainer _spellPowerStorage { get; init; }
        MagicConfig _magicConfig { get; init; }
        StatsStorage _statsStorage { get; init; }

        public SpellDescriptionConstructor(KnownSpellData spellData, StatsStorage statsStorage, MagicConfig magicConfig)
        {
            _spellData = spellData;
            _statsStorage = statsStorage;
            _spellPowerStorage = magicConfig.FindSpellPowerStorage(statsStorage);
            _magicConfig = magicConfig;
        }

        public string ConstructDescription()
        {
            var abilityMods = GetSpellModifiers(_spellData.activeEffects);
            return _spellData.spellEffect.GetDescription(abilityMods);
        }

        public string ConstructDescriptionWith(int slotIndex, SpellString spellString)
        {
            var newSpellData = _spellData.CreateDeepCopy();
            newSpellData.SetActiveString(slotIndex, spellString);
            return ConstructDescription(_spellData, newSpellData);
        }

        public string GetRankUpDescription()
        {
            var newSpellData = _spellData.CreateDeepCopy();
            newSpellData.IncreaseRank();
            return ConstructDescription(_spellData, newSpellData);
        }

        public string GetSpellCostWith(int slotIndex, SpellString spellString)
        {
            var newSpellData = _spellData.CreateDeepCopy();
            newSpellData.SetActiveString(slotIndex, spellString);
            return GetSpellCostText(_spellData, newSpellData);
        }

        public string GetRankUpSpellCost()
        {
            var newSpellData = _spellData.CreateDeepCopy();
            newSpellData.IncreaseRank();
            return GetSpellCostText(_spellData, newSpellData);
        }

        private string ConstructDescription(KnownSpellData oldSpell, KnownSpellData newSpell)
        {
            var oldAbilityMods = GetSpellModifiers(oldSpell.activeEffects);
            var newAbilityMods = GetSpellModifiers(newSpell.activeEffects);
            return oldSpell.spellEffect.GetDescription(oldAbilityMods) + "->\n" + newSpell.spellEffect.GetDescription(newAbilityMods);
        }

        private string GetSpellCostText(KnownSpellData oldSpell, KnownSpellData newSpell)
        {
            int oldSpellCost = _magicConfig.GetSpellCost(oldSpell, _statsStorage);
            int newSpellCost = _magicConfig.GetSpellCost(newSpell, _statsStorage);
            if (oldSpellCost == newSpellCost)
            {
                return oldSpellCost.ToString();
            }
            else
            {
                return $"{oldSpellCost} -> {newSpellCost}";
            }
        }

        private AbilityModifiers GetSpellModifiers(IEffectsIterator spellData)
        {
            int rawSpellPower = _spellPowerStorage.GetAdjustedValue(spellData);

            return new AbilityModifiers
            {
                magnitudeMult = rawSpellPower * 0.01f,
            };
        }
    }
}