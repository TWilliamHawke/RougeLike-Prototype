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
    public class KnownSpellData : IAbilitySource, IActionBearer
    {
        delegate int SelectSpellLinesBuff(SpellString spellString);
        public static event UnityAction<KnownSpellData> OnChangeData;

        const int MAX_SPELL_RANK = 6;
        public string displayName { get; init; }

        public int rank => _rank;
        public SpellString[] activeStrings => _activeStrings;
        public int manaCost => CalculateManaCost();
        public bool spellHasMaxRank => _rank >= MAX_SPELL_RANK;
        public Sprite icon => _spell.icon;
        public Ability spellEffect => _spell[rank].spellEffect;

        Spell _spell;

        int _rank = 1;
        SpellString[] _activeStrings;



        private int CalculateManaCost()
        {
            //min = 10%
            float costMult = Mathf.Max(1 + GetBuffFromSpellLines(s => s.manaCostMod) / 100f, 0.1f);
            return Mathf.RoundToInt(_spell[_rank].manaCost * costMult);
        }


        public KnownSpellData(Spell spell)
        {
            _spell = spell;
            _rank = spell.startRank;
            displayName = spell.displayName;
            _activeStrings = new SpellString[6];
        }

        public KnownSpellData(Spell spell, string customName) : this(spell)
        {
            displayName = customName;
        }

        public KnownSpellData CreateCopy(string name)
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
            OnChangeData?.Invoke(this);
        }

        public IAbilityInstruction CreateAbilityInstruction()
        {
            return new SpellUsageInstruction(this);
        }

        public string ConstructDescription()
        {
            float powerMult = 1 + GetBuffFromSpellLines(s => s.spellPowerMod) / 100f;
            return spellEffect.GetDescription(new AbilityModifiers(powerMult));
        }

        public void SetActiveString(int slotIndex, SpellString spellString)
        {
            if (slotIndex >= _activeStrings.Length) return;
            _activeStrings[slotIndex] = spellString;
            OnChangeData?.Invoke(this);
        }

        int GetBuffFromSpellLines(SelectSpellLinesBuff selector)
        {
            int mod = 0;

            foreach (var spellLine in _activeStrings)
            {
                if (spellLine is null) continue;
                mod += selector(spellLine);
            }

            return mod;
        }
    }
}