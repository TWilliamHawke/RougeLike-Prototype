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
    public class KnownSpellData : IAbilitySource, IEffectsIterator, IInjectionTarget
    {
        delegate int SelectSpellLinesBuff(SpellString spellString);
        public event UnityAction OnDataChange;

        const int MAX_SPELL_RANK = 6;

        public string displayName { get; set; }
        public string baseName => _spell.displayName;

        public int rank => _rank;
        public int manaCost => CalculateManaCost();
        public bool spellHasMaxRank => _rank >= MAX_SPELL_RANK;
        public Sprite icon => _spell.icon;
        public Ability spellEffect => _spell.GetEffectAt(rank);
        public int baseManaCost => _spell.GetCostAt(rank);

        public bool waitForAllDependencies => false;

        Spell _spell;

        int _rank = 1;
        StringSlotData[] _activeStrings = new StringSlotData[6];
        EffectContainer _effectContainer = new();

        [InjectField] SpellUsageController _playerSpellController;

        public KnownSpellData(Spell spell, Injector injector) : this(spell)
        {
            injector.AddInjectionTarget(this);
        }

        private KnownSpellData(Spell spell)
        {
            _spell = spell;
            _rank = spell.startRank;
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

            _rank++;
            OnDataChange?.Invoke();
        }

        public string ConstructDescription()
        {
            float powerMult = 1 + GetBuffsFromSpellLines(s => s.spellPowerMod) / 100f;
            return spellEffect.GetDescription(new AbilityModifiers(powerMult));
        }

        public IAbilityInstruction CreateAbilityInstruction()
        {
            return new SpellUsageInstruction(this);
        }

        public bool StringSlotIsEmpty(int idx)
        {
            if (!IndexIsCorrect(idx)) return false;
            return _activeStrings[idx].IsEmpty();
        }

        public void SetActiveString(int slotIndex, SpellString spellString)
        {
            if (!IndexIsCorrect(slotIndex)) return;
            StringSlotData slot = new(spellString, slotIndex);
            _activeStrings[slotIndex] = slot;

            foreach (var effect in spellString.effects)
            {
                _effectContainer.AddEffect(slot, effect);
            }
            OnDataChange?.Invoke();
        }

        public void ClearStringSlot(int idx)
        {
            if (StringSlotIsEmpty(idx)) return;
            var slot = _activeStrings[idx];
            _effectContainer.RemoveEffect(slot);
            slot.Clear();
            OnDataChange?.Invoke();
        }

        public IEnumerable<SpellString> GetActiveStrings()
        {
            foreach (var stringSlot in _activeStrings)
            {
                if (stringSlot.IsEmpty()) continue;
                yield return stringSlot.spellString;
            }
        }

        public SpellString GetSpellStringAt(int slotIndex)
        {
            if (StringSlotIsEmpty(slotIndex)) return null;
            return _activeStrings[slotIndex].spellString;
        }

        public IEnumerable<IStaticEffectData> GetEffects(IEffectSignature type)
        {
            return _effectContainer.GetEffects(type);
        }

        public IEnumerable<IStaticEffectData> GetEffects()
        {
            return _effectContainer.GetEffects();
        }

        public void FinalizeInjection()
        {
            _playerSpellController.OnMonoDestroy += RemoveSpellController;
        }

        private bool IndexIsCorrect(int idx)
        {
            return idx < _activeStrings.Length && idx >= 0;
        }

        private int CalculateManaCost()
        {
            return _playerSpellController?.GetSpellCost(this) ?? baseManaCost;
        }

        private void RemoveSpellController()
        {
            _playerSpellController = null;
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