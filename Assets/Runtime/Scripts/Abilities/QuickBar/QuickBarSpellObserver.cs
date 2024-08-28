using System.Collections.Generic;
using Magic;
using UnityEngine;

namespace Abilities
{
    public class QuickBarSpellObserver : MonoBehaviour, IObserver<IAbilityContainer>
    {
        [SerializeField] Spellbook _spellbook;
        [SerializeField] QuickBarDataStorage _quickBarDataStorage;

        HashSet<SpellUsageInstruction> _activeSpells = new();

        void Awake()
        {
            _quickBarDataStorage.AddSlotObserver(this);
            _spellbook.OnSpellRemoved += RemoveSpellFromQuickBar;
        }

        void OnDestroy()
        {
            _spellbook.OnSpellRemoved -= RemoveSpellFromQuickBar;
            _quickBarDataStorage.RemoveSlotObserver(this);
        }

        public void AddToObserve(IAbilityContainer target)
        {
            if (target is SpellUsageInstruction instruction)
            {
                _activeSpells.Add(instruction);
            }
        }

        public void RemoveFromObserve(IAbilityContainer target)
        {
            if (target is SpellUsageInstruction instruction)
            {
                _activeSpells.Remove(instruction);
            }
        }

        private void RemoveSpellFromQuickBar(KnownSpellData data)
        {
            foreach (var instruction in _activeSpells)
            {
                if (instruction.HasSpell(data))
                {
                    _quickBarDataStorage.RemoveAbility(instruction);
                    //everything else will be done in RemoveFromObserve
                }
            }
        }
    }
}