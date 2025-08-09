using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Abilities
{
    public class QuickBarDataStorage : ScriptableObject, IEnumerable<IAbilityContainer>
    {
        const int MAX_QUICK_ABILITIES = 10;

        public event UnityAction OnQuickBarChange;
        public event UnityAction<IAbilityContainer> OnAbilityAdded;
        public event UnityAction<IAbilityContainer> OnAbilityRemoved;

        public IAbilityContainer mainAbility => _mainAbility;

        IAbilityContainer _mainAbility;

        IAbilityContainer[] _quickAbilities = new IAbilityContainer[MAX_QUICK_ABILITIES];

        private void OnEnable()
        {
            _mainAbility = null;
            for (int i = 0; i < _quickAbilities.Length; i++)
            {
                _quickAbilities[i] = null;
            }
        }

        public void SetQuickAbility(int index, IAbilityContainer ability)
        {
            if (!IndexIsCorrect(index))return;

            TryRemoveQuickAbility(index);
            _quickAbilities[index] = ability;
            OnAbilityAdded?.Invoke(ability);
            OnQuickBarChange?.Invoke();
        }

        public void SetMainAbility(IAbilityContainer ability)
        {
            TryRemoveMainAbility();
            _mainAbility = ability;
            OnAbilityAdded?.Invoke(ability);
            OnQuickBarChange?.Invoke();
        }

        public bool TryGetQuickAbility(int index, out IAbilityContainer ability)
        {
            ability = default;
            if (IndexIsCorrect(index) && _quickAbilities[index] != null)
            {
                ability = _quickAbilities[index];
                return true;
            }
            return false;
        }

        public void RemoveAbility(IAbilityContainer ability)
        {
            bool isSuccess = false;

            for (int i = 0; i < _quickAbilities.Length; i++)
            {
                if (_quickAbilities[i] != ability) continue;
                isSuccess = TryRemoveQuickAbility(i) || isSuccess;
            }

            if (_mainAbility != ability) return;
            isSuccess = TryRemoveMainAbility() || isSuccess;

            if (!isSuccess) return;
            OnQuickBarChange?.Invoke();
        }

        private bool TryRemoveQuickAbility(int index)
        {
            if (!IndexIsCorrect(index) || _quickAbilities[index] == null) return false;
            OnAbilityRemoved?.Invoke(_quickAbilities[index]);
            _quickAbilities[index] = null;
            return true;
        }

        private bool TryRemoveMainAbility()
        {
            if (_mainAbility == null) return false;
            OnAbilityRemoved?.Invoke(_mainAbility);
            _mainAbility = null;
            return true;
        }

        private bool IndexIsCorrect(int index)
        {
            return index >= 0 && index < MAX_QUICK_ABILITIES;
        }

        public IEnumerator<IAbilityContainer> GetEnumerator()
        {
            if(_mainAbility != null) yield return _mainAbility;
            for (int i = 0; i < _quickAbilities.Length; i++)
            {
                if (_quickAbilities[i] == null) continue;
                yield return _quickAbilities[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}