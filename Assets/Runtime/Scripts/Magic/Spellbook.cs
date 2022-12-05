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
        public event UnityAction<KnownSpellData> OnSpellPageOpen;
        public event UnityAction<KnownSpellData> OnSpellSelect;

        [SerializeField] Inventory _inventory;
        [SerializeField] int _increaseRankCost = 500;

        List<KnownSpellData> _knownSpells;

        public List<KnownSpellData> knownSpells => _knownSpells;
        public int maxSpellRank => 6;
        public int increaseRankCost => _increaseRankCost;

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


        public void AddSpell(Spell spell)
        {
            _knownSpells.Add(new KnownSpellData(spell));
            OnSpellAdded?.Invoke(spell);
        }

        public bool SpellIsKnown(Spell spell)
        {
            return _knownSpells.Any(spelldata => spelldata.spell == spell);
        }

        public void Clear()
        {
            _knownSpells.Clear();
        }

        public void OpenSpellPage(KnownSpellData spell)
        {
            OnSpellPageOpen?.Invoke(spell);
        }

        public void SelectSpell(KnownSpellData spell)
        {
            OnSpellSelect?.Invoke(spell);
        }

        public void IncreaseSpellRank(KnownSpellData spell)
        {
            if (_inventory.resources.TrySpendResource(ResourceType.magicDust, _increaseRankCost))
            {
                spell.IncreaseRank();
            }
        }
    }
}