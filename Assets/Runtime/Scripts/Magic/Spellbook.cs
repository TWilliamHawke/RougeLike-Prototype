using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Entities.Player;
using Items;

namespace Magic
{
    public class Spellbook : ScriptableObject
    {
        public event UnityAction OnSpellAdded;
        public event UnityAction<KnownSpellData> OnSpellPageOpen;
        public event UnityAction<KnownSpellData> OnSpellSelect;

        [SerializeField] StoredResources _resources;
        [SerializeField] int _increaseRankCost = 500;

        List<KnownSpellData> _knownSpells;

        public List<KnownSpellData> knownSpells => _knownSpells;
        public int maxSpellRank => 6;
        public int increaseRankCost => _increaseRankCost;


        public void AddSpell(Spell spell)
        {
            _knownSpells.Add(new KnownSpellData(spell));
            OnSpellAdded?.Invoke();
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
            if (_resources.TrySpendResource(ResourceType.magicDust, _increaseRankCost))
            {
                spell.IncreaseRank();
            }
        }
    }
}