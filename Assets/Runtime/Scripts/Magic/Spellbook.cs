using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Magic
{
    public class Spellbook : ScriptableObject
    {
        public event UnityAction OnSpellAdded;
        public event UnityAction<KnownSpellData> OnSpellPageOpen;

        List<KnownSpellData> _knownSpells;

        public List<KnownSpellData> knownSpells => _knownSpells;

        public void AddSpell(Spell spell)
        {
            _knownSpells.Add(new KnownSpellData(spell));
            OnSpellAdded?.Invoke();
        }

        public void Clear()
        {
            _knownSpells.Clear();
        }

        public void OpenSpellPage(KnownSpellData data)
        {
            OnSpellPageOpen?.Invoke(data);
        }
    }
}