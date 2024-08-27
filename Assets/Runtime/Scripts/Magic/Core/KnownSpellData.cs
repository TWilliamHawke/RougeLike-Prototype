using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.Events;
using Effects;
using Core.UI;
using Magic.UI;

namespace Magic
{
    [System.Serializable]
    public class KnownSpellData : IAbilitySource, IInjectionTarget, IContextMenuData
    {
        delegate int SelectSpellLinesBuff(SpellString spellString);
        public event UnityAction OnDataChange;

        const int MAX_SPELL_RANK = 6;

        public string displayName { get; set; }
        public string baseName => _spell.displayName;

        public int rank { get; private set; }
        
        public int manaCost => CalculateManaCost();
        public bool spellHasMaxRank => rank >= MAX_SPELL_RANK;
        public Sprite icon => _spell.icon;
        public Ability spellEffect => _spell.GetEffectAt(rank);

        public bool waitForAllDependencies => false;

        Spell _spell { get; init; }

        ActiveStrings _activeStrings = new();

        [InjectField] SpellUsageController _playerSpellController;

        public KnownSpellData(Spell spell, Injector injector) : this(spell)
        {
            injector.AddInjectionTarget(this);
        }

        private KnownSpellData(Spell spell)
        {
            _spell = spell;
            rank = spell.startRank;
            displayName = spell.displayName;
        }

        public KnownSpellData CreateCopy(string name)
        {
            KnownSpellData data = new(_spell);
            data.displayName = name;
            data._playerSpellController = _playerSpellController;
            return data;
        }

        public bool SpellIsTheSame(Spell spell)
        {
            return spell == _spell;
        }

        public bool SpellIsTheSame(KnownSpellData other)
        {
            return other._spell == _spell;
        }

        public void IncreaseRank()
        {
            if (spellHasMaxRank) return;

            rank++;
            OnDataChange?.Invoke();
        }

        public string ConstructDescription()
        {
            var abilityMods = _playerSpellController.GetSpellModifiers(_activeStrings);
            return spellEffect.GetDescription(abilityMods);
        }

        public string ConstructDescriptionWith(SpellString spellString)
        {
            var oldAbilityMods = _playerSpellController.GetSpellModifiers(_activeStrings);
            var newAbilityMods = _activeStrings.GetSpellModifiersWith(_playerSpellController, spellString);

            //TODO replace with ability progress description like [1-2] => [2-3]
            return spellEffect.GetDescription(oldAbilityMods) + "->\n" + spellEffect.GetDescription(newAbilityMods);
        }

        public IAbilityInstruction CreateAbilityInstruction()
        {
            return new SpellUsageInstruction(this);
        }

        public bool StringSlotIsEmpty(int idx)
        {
            return _activeStrings.StringSlotIsEmpty(idx);
        }

        public void SetActiveString(int slotIndex, SpellString spellString)
        {
            _activeStrings.SetActiveString(slotIndex, spellString);
            OnDataChange?.Invoke();
        }

        public void ClearStringSlot(int idx, Inventory inventory)
        {
            _activeStrings.ClearStringSlot(idx, inventory);
            OnDataChange?.Invoke();
        }

        public void ClearAllSlots(Inventory inventory)
        {
            _activeStrings.ClearAllSlots(inventory);
            OnDataChange?.Invoke();
        }

        public StringSlotData GetSpellSlotAt(int slotIndex)
        {
            return _activeStrings.GetSpellSlotAt(slotIndex);
        }

        public void FinalizeInjection()
        {
            _playerSpellController.OnMonoDestroy += RemoveSpellController;
        }

        private int CalculateManaCost()
        {
            int baseManaCost = _spell.GetCostAt(rank);
            return _playerSpellController?.GetSpellCost(baseManaCost, _activeStrings) ?? baseManaCost;
        }

        private void RemoveSpellController()
        {
            _playerSpellController = null;
        }
    }
}