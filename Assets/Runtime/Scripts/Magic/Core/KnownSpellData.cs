using UnityEngine;
using Items;
using UnityEngine.Events;
using Effects;
using Core.UI;
using Abilities;

namespace Magic
{
    [System.Serializable]
    public class KnownSpellData : IAbilitySource
    {
        delegate int SelectSpellLinesBuff(SpellString spellString);
        public event UnityAction OnDataChange;

        const int MAX_SPELL_RANK = 6;

        public string displayName { get; set; }
        public string baseName => _spell.displayName;

        public int rank { get; private set; }
        
        public bool spellHasMaxRank => rank >= MAX_SPELL_RANK;
        public Sprite icon => _spell.icon;
        public IAbility spellEffect => _spellEffect;
        public int baseManaCost => _spell.GetCostAt(rank);
        public IEffectsIterator activeEffects => _activeStrings;

        Spell _spell { get; init; }
        ActiveStrings _activeStrings = new();
        IAbility _spellEffect;

        public KnownSpellData(Spell spell)
        {
            _spell = spell;
            rank = spell.startRank;
            displayName = spell.displayName;
            _spellEffect = spell.GetEffectAt(rank).CreateAbility();
        }

        //without rank and spell slots
        public KnownSpellData CreateCopy(string name)
        {
            KnownSpellData data = new(_spell);
            data.displayName = name;
            return data;
        }

        public KnownSpellData CreateDeepCopy()
        {
            KnownSpellData data = new(_spell);
            data.displayName = displayName;
            data.rank = rank;
            data._activeStrings = _activeStrings.Clone();
            data._spellEffect = _spellEffect;
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
            _spellEffect = _spell.GetEffectAt(rank).CreateAbility();
            OnDataChange?.Invoke();
        }

        public IAbilityContainer CreateAbilityContainer(IAbilitiesFactory factory)
        {
            return factory.CreateSpellContainer(this);
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
    }
}