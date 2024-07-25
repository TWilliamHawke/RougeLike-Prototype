using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Entities.PlayerScripts;
using Items;
using System.Linq;

namespace Magic
{
    public class Spellbook : ScriptableObject
    {
        public event UnityAction<Spell> OnSpellAdded;
        public event UnityAction OnSpellAddedFailure;
        public event UnityAction OnUpdate;

        [SerializeField] Inventory _inventory;
        [SerializeField] Injector _playerSpellController;
        [SerializeField] int _increaseRankCost = 500;
        [SerializeField] int _clearSlotCost = 25;

        List<KnownSpellData> _knownSpells;

        public List<KnownSpellData> knownSpells => _knownSpells;
        public int maxSpellRank => 6;
        public int clearSlotCost => _clearSlotCost;
        public int increaseRankCost => _increaseRankCost;
        public int totalCount => _knownSpells.Count;
        public KnownSpellData this[int idx] => _knownSpells[idx];

        public bool TryAddSpell(Spell spell)
        {
            bool spellisKnown = SpellIsKnown(spell);
            if (spellisKnown)
            {
                OnSpellAddedFailure?.Invoke();
            }
            else
            {
                AddSpell(spell);
            }

            return !spellisKnown;
        }

        public void AddSpellCopy(KnownSpellData spell)
        {
            int spellCount = 1 + _knownSpells.Count(spellData => spellData.SpellIsTheSame(spell));
            var newSpell = spell.CreateCopy($"{spell.baseName} {spellCount}");
            int idx = _knownSpells.FindLastIndex(spellData => spellData.SpellIsTheSame(spell));

            _knownSpells.Insert(idx + 1, newSpell);
            OnUpdate?.Invoke();
        }

        public bool SpellIsKnown(Spell spell)
        {
            return _knownSpells.Any(spelldata => spelldata.SpellIsTheSame(spell));
        }

        public void Clear()
        {
            _knownSpells.Clear();
        }

        public int GetCountSpellsOfType(KnownSpellData spellData)
        {
            return _knownSpells.Count(spell => spell.SpellIsTheSame(spellData));
        }

        public void DeleteSpell(KnownSpellData data)
        {
            _knownSpells.Remove(data);
            OnUpdate?.Invoke();
        }

        private void AddSpell(Spell spell)
        {
            _knownSpells.Add(new KnownSpellData(spell, _playerSpellController));
            OnSpellAdded?.Invoke(spell);
        }
    }
}