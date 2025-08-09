using System.Collections.Generic;
using Magic;
using UnityEngine;

namespace Abilities
{
    public class QuickBarSpellCleaner : MonoBehaviour, IObserver<IAbilityContainer>
    {
        [SerializeField] Spellbook _spellbook;
        [SerializeField] QuickBarDataStorage _quickBarDataStorage;
        [SerializeField] QuickBarObserversController _observersController;

        HashSet<SpellContainer> _activeSpells = new();

        void Awake()
        {
            _observersController.AddSlotObserver(this);
            _spellbook.OnSpellRemoved += RemoveSpellFromQuickBar;
        }

        void OnDestroy()
        {
            _spellbook.OnSpellRemoved -= RemoveSpellFromQuickBar;
        }

        public void AddToObserve(IAbilityContainer target)
        {
            if (target is SpellContainer container)
            {
                _activeSpells.Add(container);
            }
        }

        public void RemoveFromObserve(IAbilityContainer target)
        {
            if (target is SpellContainer container)
            {
                _activeSpells.Remove(container);
            }
        }

        private void RemoveSpellFromQuickBar(KnownSpellData data)
        {
            foreach (var container in _activeSpells)
            {
                if (container.HasSpell(data))
                {
                    _quickBarDataStorage.RemoveAbility(container);
                    //everything else will be done in RemoveFromObserve
                }
            }
        }
    }
}