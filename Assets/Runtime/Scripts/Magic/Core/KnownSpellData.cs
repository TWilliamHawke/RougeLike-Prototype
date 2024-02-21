using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.Events;
using Effects;
using Core.UI;

namespace Magic
{
    [System.Serializable]
    public class KnownSpellData : IAbilitySource
    {
        delegate int SelectSpellLinesBuff(SpellString spellString);
        public event UnityAction OnChangeData;

        const int MAX_SPELL_RANK = 6;
        public string displayName { get; init; }
        public string baseName => _spell.displayName;

        public int rank => _rank;
        public StringSlotData[] activeStrings => _activeStrings;
        public int manaCost => CalculateManaCost();
        public bool spellHasMaxRank => _rank >= MAX_SPELL_RANK;
        public Sprite icon => _spell.icon;
        public Ability spellEffect => _spell.GetEffectAt(rank);
        public int baseManaCost => _spell.GetCostAt(rank);

        Spell _spell;

        int _rank = 1;
        StringSlotData[] _activeStrings = new StringSlotData[6];


        public KnownSpellData(Spell spell)
        {
            _spell = spell;
            _rank = spell.startRank;
            displayName = spell.displayName;
        }

        public KnownSpellData(Spell spell, string customName) : this(spell)
        {
            displayName = customName;
        }

        public KnownSpellData CreateNewSpellData(string name)
        {
            return new KnownSpellData(_spell, name);
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
            if (_rank >= MAX_SPELL_RANK) return;

            _rank++;
            OnChangeData?.Invoke();
        }

        public bool StringSlotIsEmpty(int idx)
        {
            if (!IndexIsCorrect(idx)) return false;
            return _activeStrings[idx].IsEmpty();
        }

        public IAbilityInstruction CreateAbilityInstruction()
        {
            return new SpellUsageInstruction(this);
        }

        public string ConstructDescription()
        {
            float powerMult = 1 + GetBuffsFromSpellLines(s => s.spellPowerMod) / 100f;
            return spellEffect.GetDescription(new AbilityModifiers(powerMult));
        }

        public void SetActiveString(int slotIndex, SpellString spellString)
        {
            if (!IndexIsCorrect(slotIndex)) return;
            _activeStrings[slotIndex] = new(spellString, slotIndex);
            OnChangeData?.Invoke();
        }

        public void ClearStringSlot(int idx)
        {
            if (!IndexIsCorrect(idx)) return;
            _activeStrings[idx].Clear();
            OnChangeData?.Invoke();
        }

        private bool IndexIsCorrect(int idx)
        {
            return idx < _activeStrings.Length && idx >= 0;
        }

        private int CalculateManaCost()
        {
            //min = 10%
            float costMult = Mathf.Max(1 + GetBuffsFromSpellLines(s => s.manaCostMod) / 100f, 0.1f);
            return Mathf.RoundToInt(baseManaCost * costMult);
        }

        private int GetBuffsFromSpellLines(SelectSpellLinesBuff selector)
        {
            int mod = 0;

            foreach (var stringSlot in _activeStrings)
            {
                if (stringSlot.IsEmpty()) continue;
                mod += selector(stringSlot.spellString);
            }

            return mod;
        }

    }
}