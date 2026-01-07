using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

namespace Abilities
{
    public class AbilitySectionData : IUISectionData<IAbilityContainer>
    {
        public int filledSlotsCount => _abilities.Count;
        public int capacity => -1;
        public string sectionName => _sectionName;
        public bool isEmpty => _abilities.Count == 0;

        public event UnityAction OnSectionDataChange;

        string _sectionName;
        List<IAbilityContainer> _abilities = new();

        public AbilitySectionData(string sectionName)
        {
            _sectionName = sectionName;
        }

        public void AddMainSlotAbility(IAbilityContainer abilityContainer)
        {
            if (!abilityContainer.fitForMainSlot) return;
            _abilities.Add(abilityContainer);
            OnSectionDataChange?.Invoke();
        }

        public void Clear()
        {
            _abilities.Clear();
            OnSectionDataChange?.Invoke();
        }

        public IEnumerable<IAbilityContainer> GetElements()
        {
            return _abilities;
        }
    }
}