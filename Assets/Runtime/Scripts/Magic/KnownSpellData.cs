using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Items;
using UnityEngine.Events;
using Effects;

namespace Magic
{
    [System.Serializable]
    public class KnownSpellData : IAbilitySource
    {
        delegate int SelectSpellLinesBuff(SpellString spellString);
        public static event UnityAction<KnownSpellData> OnChangeData;

        //other classes shouldn't read spell data directly
        public int rank => _rank;
        public SpellString[] activeStrings => _activeStrings;
        public int manaCost => CalculateManaCost();

        Spell _spell;

        int _rank = 1;
        SpellString[] _activeStrings;



        private int CalculateManaCost()
        {
            //min = 10%
            float costMult = Mathf.Max(1 + GetBuffFromSpellLines(s => s.manaCostMod) / 100f, 0.1f);
            return Mathf.RoundToInt(_spell[_rank].manaCost * costMult);
        }

        public string displayName { get; init; }
        public Sprite icon => _spell.icon;
        public Ability spellEffect => _spell[rank].spellEffect;

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

        public bool ContainSpell(Spell spell)
        {
            return spell == _spell;
        }

        public void IncreaseRank()
        {
            if (_rank >= 6) return;

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