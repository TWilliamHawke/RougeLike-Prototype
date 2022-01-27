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
        public static event UnityAction<KnownSpellData> OnChangeData;

        delegate int SelectSpellMod(SpellString spellString);

        Spell _spell;
        int _rank = 1;
        SpellString[] _activeStrings;

        //other classes shouldn't read spell data directly
        public int rank => _rank;
        public SpellString[] activeStrings => _activeStrings;
        public int manaCost => CalculateManaCost();

        private int CalculateManaCost()
        {
			//min = 10%
			float costMult = Mathf.Max(1 + GetSpellMod(s => s.manaCostMod) / 100f, 0.1f);
            return Mathf.RoundToInt(_spell[_rank].manaCost * costMult);
        }

        public string displayName => _spell.displayName;
        public Sprite icon => _spell.icon;
        public Ability spell => _spell[rank].spellEffect;

        public KnownSpellData(Spell spell)
        {
            _spell = spell;
            _rank = _spell.startRank;
            _activeStrings = new SpellString[6];
        }

        public void IncreaseRank()
        {
            if (_rank >= 6) return;

            _rank++;
            OnChangeData?.Invoke(this);
        }

        public IAbilityInstruction CreateAbilityInstruction()
        {
            return new SpellContainer(this);
        }

        public string ConstructDescription()
        {
            float powerMult = 1 + GetSpellMod(s => s.spellPowerMod) / 100f;
            return spell.GetDescription(new AbilityModifiers(powerMult));
        }

        public void SetActiveString(int slotIndex, SpellString spellString)
        {
            if (slotIndex >= _activeStrings.Length) return;
            _activeStrings[slotIndex] = spellString;
            OnChangeData?.Invoke(this);
        }

        int GetSpellMod(SelectSpellMod selector)
        {
            int mod = 0;

            foreach (var spellString in _activeStrings)
            {
                if (spellString is null) continue;
                mod += selector(spellString);
            }

            return mod;
        }
    }
}