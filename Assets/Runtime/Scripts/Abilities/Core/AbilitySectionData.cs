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

        string _sectionName;

        public AbilitySectionData(string sectionName)
        {
            _sectionName = sectionName;
        }

        List<IAbilityContainer> _abilities = new();

        public event UnityAction OnSectionDataChange;

        public void AddAbility(IAbilityContainer abilityContainer)
        {
            _abilities.Add(abilityContainer);
            OnSectionDataChange?.Invoke();
        }

        public void Clear()
        {
            _abilities.Clear();
            OnSectionDataChange?.Invoke();
        }

        public IEnumerator<IAbilityContainer> GetEnumerator()
        {
            return _abilities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _abilities.GetEnumerator();
        }
    }
}